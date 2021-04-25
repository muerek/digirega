using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetCrewChangeDto : GetEntryDto
    {
        public int BowNumber { get; set; } = 0;

        public IList<GetSubstitutionDto> Substitutions { get; set; } = new List<GetSubstitutionDto>();
    }
}
