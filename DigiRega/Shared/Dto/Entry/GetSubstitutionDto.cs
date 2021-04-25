using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetSubstitutionDto
    {
        public GetAthleteDto Old { get; set; } = new();

        public GetAthleteDto New { get; set; } = new();
    }
}
