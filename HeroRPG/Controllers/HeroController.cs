using HeroRPG.Dtos.Hero;
using HeroRPG.Models;
using HeroRPG.Services.HeroService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeroRPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _heroService.GetAllHeroes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetSingleHero(int id)
        {
            return Ok(await _heroService.GetHeroById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> AddHero(AddHeroDto newHero)
        {
            return Ok(await _heroService.AddHero(newHero));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> DeleteHero(int id)
        {
            var response = await _heroService.DeleteHero(id);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> UpdateHero(UpdateHeroDto updateHero)
        {
            var response = await _heroService.UpdateHero(updateHero);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> AddHeroSkill(AddHeroSkillDto newHeroSkill)
        {
            return Ok(await _heroService.AddHeroSkill(newHeroSkill));
        }
    }
}
