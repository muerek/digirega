using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class AddLateEntryDto : AddEntryDto
    {
        [ValidateComplexType]
        public IList<AddCrewMemberDto> Crew { get; set; } = new List<AddCrewMemberDto>();
    }
}
