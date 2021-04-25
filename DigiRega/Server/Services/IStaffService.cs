using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.StaffMember;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IStaffService
    {
        Task<ServiceResponse<IList<GetStaffMemberDto>>> GetAllStaffAsync();

        Task<ServiceResponse<GetStaffMemberDto>> GetStaffMemberAsync(int id);

        Task<ServiceResponse<GetStaffMemberDto>> GetStaffMemberAsync(string username);

        Task<ServiceResponse<int>> AddStaffMemberAsync(AddStaffMemberDto staffMemberDto);

        Task<ServiceResponse> UpdateStaffMemberAsync(int id, UpdateStaffMemberDto update);

        Task<ServiceResponse> RemoveStaffMemberAsync(int id);
    }
}
