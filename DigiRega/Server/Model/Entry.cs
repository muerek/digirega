using DigiRega.Shared.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents common metadata for all entries.
    /// </summary>
    public class Entry
    {
        public int Id { get; set; } = 0;
        
        /// <summary>
        /// Time the entry was sent to the OC.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Manager responsible of this entry.
        /// </summary>
        public Manager SentBy { get; set; } = null!;
        public int SentById { get; set; } = 0;

        /// <summary>
        /// Entry status in the processing workflow.
        /// </summary>
        public EntryStatus Status { get; set; } = EntryStatus.New;

        /// <summary>
        /// Club presenting this entry.
        /// </summary>
        /// <remarks>
        /// This works as long as mixed-club entries are not allowed, i.e. U15.
        /// </remarks>
        public virtual Club Club { get; set; } = null!;
        public int ClubId { get; set; } = 0;

        /// <summary>
        /// Race this entry belongs to.
        /// </summary>
        public virtual Race Race { get; set; } = null!;
        public int RaceId { get; set; } = 0;
    }
}
