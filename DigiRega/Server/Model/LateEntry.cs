using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    public class LateEntry : Entry
    {
        public IList<CrewMember> Crew { get; set; } = new List<CrewMember>();
    }
}
