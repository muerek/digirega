using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Entry
{
    public class GetWithdrawalDto : GetEntryDto
    {
        public int BowNumber { get; set; } = 0;
    }
}
