using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetCrewMemberDto
    {
        /// <summary>
        /// Position in the crew. Set to 0 for cox.
        /// </summary>
        public int Position { get; set; } = 1;

        public GetAthleteDto Athlete { get; set; } = new();
    }
}
