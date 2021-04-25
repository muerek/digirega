using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    public class Withdrawal : Entry
    {
        /// <summary>
        /// Bow number of the crew withdrawing per the official list of entries.
        /// </summary>
        public int BowNumber { get; set; } = 0;
    }
}
