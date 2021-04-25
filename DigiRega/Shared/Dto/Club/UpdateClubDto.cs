using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Club
{
    public class UpdateClubDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
