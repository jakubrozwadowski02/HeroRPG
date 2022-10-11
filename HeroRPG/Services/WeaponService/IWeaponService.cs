using HeroRPG.Dtos.Hero;
using HeroRPG.Dtos.Weapon;
using HeroRPG.Models;

namespace HeroRPG.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetHeroDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
