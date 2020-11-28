using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("Punishment")]
    public class Punishment
    {
        [Key]
        public int Id { get; set; }
        public int IdPrisoner { get; set; }
        public int IdReason { get; set; }
        [Required]
        public bool Lifery { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("IdReason")]
        public virtual Reason Reason { get; set; }
        [ForeignKey("IdPrisoner")]
        public virtual Prisoner Prisoner { get; set; }
    }
}
