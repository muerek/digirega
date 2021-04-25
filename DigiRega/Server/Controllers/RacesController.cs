using DigiRega.Server.Services;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Race;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiRega.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService raceService;

        public RacesController(IRaceService raceService)
        {
            this.raceService = raceService;
        }

        // GET: api/<RacesController>
        [HttpGet]
        public async Task<ServiceResponse<IList<GetRaceDto>>> Get()
        {
            return await raceService.GetAllRacesAsync();
        }

        // GET api/<RacesController>/5
        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetRaceDto>> Get(int id)
        {
            return await raceService.GetRaceAsync(id);
        }

        // POST api/<RacesController>
        [HttpPost]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse<int>> Post([FromBody] AddRaceDto raceDto)
        {
            return await raceService.AddRaceAsync(raceDto);
        }

        // PUT api/<RacesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse> Put(int id, [FromBody] UpdateRaceDto update)
        {
            return await raceService.UpdateRaceAsync(id, update);
        }

        // DELETE api/<RacesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await raceService.RemoveRaceAsync(id);
        }
    }
}
