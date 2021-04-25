using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Race
{
    public class UpdateRaceDto
    {
        [Required]
        public string Number { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
