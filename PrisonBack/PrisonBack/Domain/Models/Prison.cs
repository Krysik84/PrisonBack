using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonBack.Domain.Models
{
    [Table("Prison")]
    public class Prison
    {
        [Key]
        public int Id { get; set; }
        public string PrisonName { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
        public virtual ICollection<Prisoner> Prisoner { get; set; }

    }
}
