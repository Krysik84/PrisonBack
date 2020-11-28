using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("Pass")]
    public class Pass
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdUser { get; set; }
        public int IdPrisoner { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        [ForeignKey("IdPrisoner")]
        public virtual Prisoner Prisoner { get; set; }
    }
}
