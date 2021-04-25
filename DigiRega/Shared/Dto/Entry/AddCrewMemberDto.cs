using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class AddCrewMemberDto
    {
        /// <summary>
        /// Position in the crew. Set to 0 for cox.
        /// </summary>
        [Required]
        [Range(0, 4)]
        public int Position { get; set; } = 1;

        [ValidateComplexType]
        public AddAthleteDto Athlete { get; set; } = new AddAthleteDto();
    }
}
