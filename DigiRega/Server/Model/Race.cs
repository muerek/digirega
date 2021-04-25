using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    public class Race
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
