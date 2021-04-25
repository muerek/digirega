using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class AddCrewChangeDto : AddEntryDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BowNumber { get; set; } = 0;

        [ValidateComplexType]
        public IList<AddSubstitutionDto> Substitutions { get; set; } = new List<AddSubstitutionDto>();
    }
}
