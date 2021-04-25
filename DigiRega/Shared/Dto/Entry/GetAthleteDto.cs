using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetAthleteDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int YearOfBirth { get; set; } = DateTime.Now.Year - 14;
    }
}
