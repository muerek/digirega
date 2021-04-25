using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class AddSubstitutionDto
    {
        [Required]
        [ValidateComplexType]
        public AddAthleteDto Old { get; set; } = new AddAthleteDto();

        [Required]
        [ValidateComplexType]
        public AddAthleteDto New { get; set; } = new AddAthleteDto();
    }
}
