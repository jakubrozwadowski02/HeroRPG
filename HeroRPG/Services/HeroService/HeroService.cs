using AutoMapper;
using HeroRPG.Data;
using HeroRPG.Dtos.Hero;
using HeroRPG.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HeroRPG.Services.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public HeroService(IMapper mapper, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetHeroDto>> GetHeroById(int id)
        {
            var serviceResponse = new ServiceResponse<GetHeroDto>();

            try
            {
                var dbHero = await _context.Heroes
                .Include(h => h.Weapon)
                .Include(h => h.Skills)
                .FirstOrDefaultAsync(h => h.Id == id && h.User.Id == GetUserId());

                serviceResponse.Data = _mapper.Map<GetHeroDto>(dbHero);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes()
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();

            try
            {
                var dbHeroes = await _context.Heroes
                .Where(c => c.User.Id == GetUserId())
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();

                serviceResponse.Data = dbHeroes
                    .Select(x => _mapper.Map<GetHeroDto>(x))
                    .ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> AddHero(AddHeroDto newHero)
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();

            try
            {
                var hero = _mapper.Map<Hero>(newHero);
                hero.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());


                _context.Heroes.Add(hero);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Heroes
                    .Where(x => x.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetHeroDto>(c))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero)
        {
            var serviceResponse = new ServiceResponse<GetHeroDto>();

            try
            {
                var dbHero = await _context.Heroes
                    .Include(h => h.User)
                    .FirstOrDefaultAsync(h => h.Id == updateHero.Id);

                if (dbHero.User.Id == GetUserId())
                {
                    dbHero = _mapper.Map<Hero>(updateHero);
                    //dbHero.Name = updateHero.Name;
                    //dbHero.HitPoints = updateHero.HitPoints;
                    //dbHero.Strength = updateHero.Strength;
                    //dbHero.Defense = updateHero.Defense;
                    //dbHero.Intelligence = updateHero.Intelligence;
                    //dbHero.Class = updateHero.Class;

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetHeroDto>(dbHero);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hero not found";
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> DeleteHero(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();

            try
            {
                var dbHero = await _context.Heroes
                    .FirstOrDefaultAsync(h => h.Id == id && h.User.Id == GetUserId());

                if (dbHero != null)
                {
                    _context.Heroes.Remove(dbHero);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Heroes
                        .Where(x => x.User.Id == GetUserId())
                        .Select(x => _mapper.Map<GetHeroDto>(x)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hero not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetHeroDto>> AddHeroSkill(AddHeroSkillDto newHeroSkill)
        {
            var serviceResponse = new ServiceResponse<GetHeroDto>();

            try
            {
                var dbHero = await _context.Heroes
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newHeroSkill.HeroId &&
                    c.User.Id == GetUserId());

                if (dbHero == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hero not found";
                    return serviceResponse;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newHeroSkill.SkillId);

                if (skill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Skill not found";
                    return serviceResponse;
                }

                dbHero.Skills.Add(skill);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetHeroDto>(dbHero);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
