using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// A token that can be used to refresh an active user session.
    /// </summary>
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; } = DateTime.Now;

        [NotMapped]
        public bool IsValid => DateTime.Now <= ExpiresAt;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
