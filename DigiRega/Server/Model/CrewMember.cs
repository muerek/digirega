using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents a single crew member in an entry.
    /// </summary>
    /// <remarks>
    /// This is a glorified tuple (int, Athlete). 
    /// </remarks>
    [Owned]
    public class CrewMember
    {
        /// <summary>
        /// Position in the crew. Set to 0 for cox.
        /// </summary>
        public int Position { get; set; } = 1;

        public Athlete Athlete { get; set; } = null!;
    }
}
