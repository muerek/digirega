using DigiRega.Server.Services;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Club;
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
    public class ClubsController : ControllerBase
    {
        private readonly IClubService clubService;

        public ClubsController(IClubService clubService)
        {
            this.clubService = clubService;
        }

        // GET: api/<ClubsController>
        [HttpGet]
        public async Task<ServiceResponse<IList<GetClubDto>>> Get()
        {
            return await clubService.GetAllClubsAsync();
        }

        // GET api/<ClubsController>/5
        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetClubDto>> Get(int id)
        {
            return await clubService.GetClubAsync(id);
        }

        // POST api/<ClubsController>
        [HttpPost]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse<int>> Post([FromBody] AddClubDto clubDto)
        {
            return await clubService.AddClubAsync(clubDto);
        }

        // PUT api/<ClubsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse> Put(int id, [FromBody] UpdateClubDto update)
        {
            return await clubService.UpdateClubAsync(id, update);
        }

        // DELETE api/<ClubsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff, Admin")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await clubService.RemoveClubAsync(id);
        }
    }
}
