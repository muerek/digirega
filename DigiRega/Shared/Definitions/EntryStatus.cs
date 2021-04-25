using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Definitions
{
    /// <summary>
    /// Represents the different states an entry has to go through while being processed.
    /// </summary>
    public enum EntryStatus
    {
        /// <summary>
        /// Entry was created by the author, but not reviewed by the OC yet.
        /// </summary>
        New = 0,

        /// <summary>
        /// OC has approved the entry and is now processing it.
        /// </summary>
        Approved = 20,

        /// <summary>
        /// OC has processed the entry after its approval.
        /// </summary>
        Done = 30,

        /// <summary>
        /// OC has rejected the entry and will not process it.
        /// </summary>
        Rejected = 50,
    }
}
