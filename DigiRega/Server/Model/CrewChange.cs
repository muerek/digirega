using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    public class CrewChange : Entry
    {
        /// <summary>
        /// Bow number of the crew being changed per the official list of entries.
        /// </summary>
        public int BowNumber { get; set; } = 0;

        public IList<Substitution> Substitutions { get; set; } = new List<Substitution>();
    }
}
