using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetLateEntryDto : GetEntryDto
    {
        public IList<GetCrewMemberDto> Crew { get; set; } = new List<GetCrewMemberDto>();
    }
}
