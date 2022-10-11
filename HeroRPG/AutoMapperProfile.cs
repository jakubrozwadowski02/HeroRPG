using AutoMapper;
using HeroRPG.Dtos.Fight;
using HeroRPG.Dtos.Hero;
using HeroRPG.Dtos.Skill;
using HeroRPG.Dtos.Weapon;
using HeroRPG.Models;

namespace HeroRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hero, GetHeroDto>();
            CreateMap<AddHeroDto, Hero>();
            CreateMap<UpdateHeroDto, Hero>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Hero, HighScoreDto>();
        }
    }
}
