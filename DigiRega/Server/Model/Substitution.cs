using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents a part of a crew change where one athlete is replaced by another.
    /// </summary>
    /// <remarks>
    /// This is an owned entity because it can only ever appear as part of a crew change.
    /// </remarks>
    [Owned]
    public class Substitution
    {
        /// <summary>
        /// The athlete being substituted.
        /// </summary>
        public Athlete Old { get; set; } = new Athlete();

        /// <summary>
        /// The new athlete. 
        /// </summary>
        public Athlete New { get; set; } = new Athlete();
    }
}
