using DigiRega.Server.Services;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Staff, Admin")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService managerService;

        public ManagersController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        // GET: api/<ManagerController>
        [HttpGet]
        public async Task<ServiceResponse<IList<GetManagerDto>>> Get([FromQuery] int? club = null)
        {
            return await managerService.GetAllManagersAsync(club);
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetManagerDto>> Get(int id)
        {
            return await managerService.GetManagerAsync(id);
        }

        // POST api/<ManagerController>
        [HttpPost]
        public async Task<ServiceResponse<int>> Post([FromBody] AddManagerDto managerDto)
        {
            return await managerService.AddManagerAsync(managerDto);
        }

        // PUT api/<ManagerController>/5
        [HttpPut("{id}")]
        public async Task<ServiceResponse> Put(int id, [FromBody] UpdateManagerDto update)
        {
            return await managerService.UpdateManagerAsync(id, update);
        }

        // DELETE api/<ManagerController>/5
        [HttpDelete("{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await managerService.RemoveManagerAsync(id);
        }
    }
}
