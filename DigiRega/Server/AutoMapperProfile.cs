using AutoMapper;
using DigiRega.Server.Model;
using DigiRega.Shared.Dto.Club;
using DigiRega.Shared.Dto.Entry;
using DigiRega.Shared.Dto.Manager;
using DigiRega.Shared.Dto.Race;
using DigiRega.Shared.Dto.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddManagerDto, Manager>();
            CreateMap<Manager, GetManagerDto>();

            CreateMap<AddStaffMemberDto, StaffMember>();
            CreateMap<StaffMember, GetStaffMemberDto>();

            CreateMap<AddClubDto, Club>();
            CreateMap<Club, GetClubDto>();

            CreateMap<AddRaceDto, Race>();
            CreateMap<Race, GetRaceDto>();

            // Brief is the same for all entry types, so the type itself has to be put in a property.
            CreateMap<Entry, GetEntryBriefDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name));
            
            CreateMap<AddEntryDto, Entry>()
                .IncludeAllDerived();
            CreateMap<Entry, GetEntryDto>()
                .IncludeAllDerived();
            
            CreateMap<AddCrewChangeDto, CrewChange>();
            CreateMap<CrewChange, GetCrewChangeDto>();

            CreateMap<AddLateEntryDto, LateEntry>();
            CreateMap<LateEntry, GetLateEntryDto>();

            CreateMap<AddWithdrawalDto, Withdrawal>();
            CreateMap<Withdrawal, GetWithdrawalDto>();

            CreateMap<AddAthleteDto, Athlete>();
            CreateMap<Athlete, GetAthleteDto>();

            CreateMap<AddSubstitutionDto, Substitution>();
            CreateMap<Substitution, GetSubstitutionDto>();

            CreateMap<AddCrewMemberDto, CrewMember>();
            CreateMap<CrewMember, GetCrewMemberDto>();
        }
    }
}
