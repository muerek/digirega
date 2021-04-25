using DigiRega.Server.Services;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.StaffMember;
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
    [Authorize(Roles = "Admin")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;

        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        // GET: api/<StaffController>
        [HttpGet]
        public async Task<ServiceResponse<IList<GetStaffMemberDto>>> Get()
        {
            return await staffService.GetAllStaffAsync();
        }

        // GET api/<StaffController>/5
        [HttpGet("{id:int}")]
        public async Task<ServiceResponse<GetStaffMemberDto>> Get(int id)
        {
            return await staffService.GetStaffMemberAsync(id);
        }

        // GET api/<StaffController>/johndoe
        [HttpGet("{username}")]
        public async Task<ServiceResponse<GetStaffMemberDto>> Get(string username)
        {
            return await staffService.GetStaffMemberAsync(username);
        }

        // POST api/<StaffController>
        [HttpPost]
        public async Task<ServiceResponse<int>> Post([FromBody] AddStaffMemberDto staffMemberDto)
        {
            return await staffService.AddStaffMemberAsync(staffMemberDto);
        }

        // PUT api/<StaffController>/5
        [HttpPut("{id}")]
        public async Task<ServiceResponse> Put(int id, [FromBody] UpdateStaffMemberDto update)
        {
            return await staffService.UpdateStaffMemberAsync(id, update);
        }

        // DELETE api/<StaffController>/5
        [HttpDelete("{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await staffService.RemoveStaffMemberAsync(id);
        }
    }
}
