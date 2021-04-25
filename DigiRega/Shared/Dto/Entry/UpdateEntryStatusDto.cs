using DigiRega.Shared.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class UpdateEntryStatusDto
    {
        /// <summary>
        /// Entry status in the processing workflow.
        /// </summary>
        [Required]
        public EntryStatus Status { get; set; } = EntryStatus.New;
    }
}
