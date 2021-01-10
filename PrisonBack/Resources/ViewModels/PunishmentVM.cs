using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources
{
    public class PunishmentVM
    {
        public int Id { get; set; }
        public int IdPrisoner { get; set; }
        public int IdReason { get; set; }
        public bool Lifery { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
