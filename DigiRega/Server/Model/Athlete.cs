using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents an athlete.
    /// </summary>
    /// <remarks>
    /// This is an owned entity because we do not have a central athlete database to reference to.
    /// As of now, athletes are merely an ephemeral container for related data.
    /// </remarks>
    [Owned]
    public class Athlete
    {        
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int YearOfBirth { get; set; } = DateTime.Now.Year - 14;
    }
}
