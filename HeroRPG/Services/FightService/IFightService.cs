using HeroRPG.Dtos.Fight;
using HeroRPG.Models;

namespace HeroRPG.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAttackDto);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto skillAttackDto);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightRequestDto);
        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();
    }
}
