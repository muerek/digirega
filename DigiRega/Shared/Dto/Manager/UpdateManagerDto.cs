using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Manager
{
    public class UpdateManagerDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        public bool SendEmail { get; set; } = false;

        // ClubId not included, do not allow club change but require deletion and adding for new club.
    }
}
