using AutoMapper;
using HeroRPG.Data;
using HeroRPG.Dtos.Fight;
using HeroRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroRPG.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FightService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttackDto)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Heroes
                    .Include(a => a.Weapon)
                    .FirstOrDefaultAsync(a => a.Id == weaponAttackDto.AttackerId);

                var opponent = await _context.Heroes
                    .FirstOrDefaultAsync(o => o.Id == weaponAttackDto.OpponentId);

                int damage = DoWeaponAttack(attacker, opponent);
                if (opponent.HitPoints <= 0)
                {
                    serviceResponse.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackertHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto skillAttackDto)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Heroes
                    .Include(a => a.Skills)
                    .FirstOrDefaultAsync(a => a.Id == skillAttackDto.AttackerId);

                var opponent = await _context.Heroes
                    .FirstOrDefaultAsync(o => o.Id == skillAttackDto.OpponentId);

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == skillAttackDto.SkillId);

                if (skill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{attacker.Name} doesn't know that skill.";
                    return serviceResponse;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoints <= 0)
                {
                    serviceResponse.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackertHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightRequestDto)
        {
            var serviceResponse = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var heroes = await _context.Heroes
                    .Include(w => w.Weapon)
                    .Include(w => w.Skills)
                    .Where(w => fightRequestDto.HeroIds.Contains(w.Id)).ToListAsync();

                bool defeated = false;

                while (!defeated)
                {
                    foreach (Hero attacker in heroes)
                    {
                        var opponents = heroes.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;

                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }

                        serviceResponse.Data.Log
                            .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            serviceResponse.Data.Log.Add($"{opponent.Name} has been defeated!");
                            serviceResponse.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }

                heroes.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        private static int DoSkillAttack(Hero? attacker, Hero? opponent, Skill? skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        private static int DoWeaponAttack(Hero? attacker, Hero? opponent)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
            var heroes = await _context.Heroes
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<HighScoreDto>>
            {
                Data = heroes.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
            };

            return serviceResponse;
        }
    }
}
