using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    [Owned]
    public class EmailRecipient
    {
        public EmailRecipient() { }

        public EmailRecipient(string name, string emailAddress)
        {
            Name = name;
            EmailAddress = emailAddress;
        }
        
        /// <summary>
        /// Display name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;
    }
}
