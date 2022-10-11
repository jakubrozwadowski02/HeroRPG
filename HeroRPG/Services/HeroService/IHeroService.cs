using HeroRPG.Dtos.Hero;
using HeroRPG.Models;

namespace HeroRPG.Services.HeroService
{
    public interface IHeroService
    {
        Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes();
        Task<ServiceResponse<GetHeroDto>> GetHeroById(int id);
        Task<ServiceResponse<List<GetHeroDto>>> AddHero(AddHeroDto newHero);
        Task<ServiceResponse<GetHeroDto>>UpdateHero(UpdateHeroDto updateHero);
        Task<ServiceResponse<List<GetHeroDto>>> DeleteHero(int id);
        Task<ServiceResponse<GetHeroDto>> AddHeroSkill(AddHeroSkillDto newHeroSkill);
    }
}
