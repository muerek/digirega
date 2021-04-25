using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    // Note that this class should be abstract as only concrete subtypes should be added.
    public abstract class AddEntryDto
    {
        public string ManagerComment { get; set; } = string.Empty;

        public string OcComment { get; set; } = string.Empty;

        /// <summary>
        /// ID of the club this entry refers to.
        /// </summary>
        /// <remarks>
        /// This must be set if the entry is added by staff or admin.
        /// If the entry is added by the manager themselves, the value can be emtpy as it will be set server-side anyway.
        /// </remarks>
        [Range(1, int.MaxValue)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ClubId { get; set; } = null;

        [Required]
        [Range(1, int.MaxValue)]
        public int RaceId { get; set; } = 0;

        /// <summary>
        /// ID of the manager responsible for this entry. 
        /// </summary>
        /// <remarks>
        /// This must be set if the entry is added by staff or admin.
        /// If the entry is added by the manager themselves, the value can be emtpy as it will be set server-side anyway.
        /// </remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SentById { get; set; } = null;

        /// <summary>
        /// Indicates if the author wants to get a copy of this entry delivered to their email address.
        /// Defaults to false if not set.
        /// </summary>
        public bool SendEmailConfirmation { get; set; } = true;
    }
}
