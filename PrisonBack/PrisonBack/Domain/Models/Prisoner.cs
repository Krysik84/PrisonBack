using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("Prisoner")]
    public class Prisoner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Forname { get; set; }
        public string Pesel { get; set; }
        public string Address { get; set; }
        public bool Pass { get; set; }
        public int Behavior { get; set; }
        public bool Isolated { get; set; }
        public int IdCell { get; set; }

        [ForeignKey("IdCell")]
        public virtual Cell Cell { get; set; }
         public virtual ICollection<Isolation> Isolations { get; set; }
        public virtual ICollection<Punishment> Punishments { get; set; }





    }
}
