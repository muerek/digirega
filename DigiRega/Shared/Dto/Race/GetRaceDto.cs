using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Race
{
    public class GetRaceDto
    {
        /// <summary>
        /// The database identifier. This is not the friendly race number.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// The friendly race number as printed in the official programme.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
