using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    public class Club
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public virtual List<Manager> Managers { get; set; } = null!;
    }
}
