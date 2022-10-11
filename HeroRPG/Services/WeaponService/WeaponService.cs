using AutoMapper;
using HeroRPG.Data;
using HeroRPG.Dtos.Hero;
using HeroRPG.Dtos.Weapon;
using HeroRPG.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HeroRPG.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetHeroDto>> AddWeapon(AddWeaponDto newWeapon)
        {
           var serviceResponse = new ServiceResponse<GetHeroDto>();

            try
            {
                var dbHero = await _context.Heroes
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.HeroId && c.User.Id == GetUserId());

                if (dbHero != null)
                {
                    var weapon = _mapper.Map<Weapon>(newWeapon);

                    _context.Weapons.Add(weapon);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetHeroDto>(dbHero);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Hero not found";
                    return serviceResponse;
                }
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
