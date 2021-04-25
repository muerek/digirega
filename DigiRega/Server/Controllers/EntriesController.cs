using DigiRega.Server.Services;
using DigiRega.Server.Utilities;
using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Entry;
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
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService entryService;

        public EntriesController(IEntryService entryService)
        {
            this.entryService = entryService;
        }

        // GET: api/<EntriesController>
        /// <summary>
        /// Gets a list of all entries. Results are ordered by most recent.
        /// </summary>
        /// <param name="status">Optional filter for entries with the specified status only.</param>
        /// <returns>List of entries.</returns>
        [HttpGet]
        public async Task<ServiceResponse<IList<GetEntryBriefDto>>> Get([FromQuery] EntryStatus? status = null)
        {
            return await entryService.GetAllEntriesAsync(status);
        }

        // GET api/<EntriesController>/5
        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetEntryDto>> Get(int id)
        {
            return await entryService.GetEntryAsync(id);
        }

        // POST api/<EntriesController>/crew-changes
        [HttpPost("crew-changes")]
        public async Task<ServiceResponse<int>> PostCrewChange([FromBody] AddCrewChangeDto crewChangeDto)
        {
            return await entryService.AddCrewChangeAsync(crewChangeDto);
        }

        // POST api/<EntriesController>/late-entries
        [HttpPost("late-entries")]
        public async Task<ServiceResponse<int>> PostLateEntry([FromBody] AddLateEntryDto lateEntryDto)
        {
            return await entryService.AddLateEntryAsync(lateEntryDto);
        }

        // POST api/<EntriesController>/withdrawals
        [HttpPost("withdrawals")]
        public async Task<ServiceResponse<int>> PostWithdrawal([FromBody] AddWithdrawalDto withdrawalDto)
        {
            return await entryService.AddWithdrawalAsync(withdrawalDto);
        }

        // PUT api/<EntriesController/5/status
        [HttpPut("{id}/status")]
        public async Task<ServiceResponse> PutStatus(int id, [FromBody] UpdateEntryStatusDto statusUpdate)
        {
            return await entryService.UpdateStatusAsync(id, statusUpdate);
        }
    }
}
