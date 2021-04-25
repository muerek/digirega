using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto.Club;
using DigiRega.Shared.Dto.Manager;
using DigiRega.Shared.Dto.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetEntryBriefDto
    {
        public int Id { get; set; } = 0;

        /// <summary>
        /// Time the entry was sent to the OC.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.Now;

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

        public string Type { get; set; } = string.Empty;
    }
}
