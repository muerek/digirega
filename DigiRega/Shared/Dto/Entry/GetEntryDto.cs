using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto.Club;
using DigiRega.Shared.Dto.Manager;
using DigiRega.Shared.Dto.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    // Custom converter to preserve polymorphism.
    [JsonConverter(typeof(GetEntryDtoJsonConverter))]
    // Note that this class should not be abstract as it will be instantiated for transport.
    public class GetEntryDto
    {
        public int Id { get; set; } = 0;

        /// <summary>
        /// Time the entry was sent to the OC.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Original author of this entry.
        /// </summary>
        public GetManagerDto SentBy { get; set; } = new();

        /// <summary>
        /// Entry status in the processing workflow.
        /// </summary>
        public EntryStatus Status { get; set; } = EntryStatus.New;

        /// <summary>
        /// Club presenting this entry.
        /// </summary>
        public GetClubDto Club { get; set; } = new();

        /// <summary>
        /// Race this entry belongs to.
        /// </summary>
        public GetRaceDto Race { get; set; } = new();
    }
}
