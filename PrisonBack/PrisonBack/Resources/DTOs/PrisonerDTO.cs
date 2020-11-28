using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources.DTOs
{
    public class PrisonerDTO
    {
        public string Name { get; set; }
        public string Forname { get; set; }
        public string Pesel { get; set; }
        public string Address { get; set; }
        public bool Pass { get; set; }
        public int Behavior { get; set; }
        public bool Isolated { get; set; }
        public int IdCell { get; set; }
    }
}
