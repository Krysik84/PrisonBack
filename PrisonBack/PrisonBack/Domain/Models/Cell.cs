using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonBack.Domain.Models
{
    [Table("Cell")]
    public class Cell
    {
        [Key]
        public int Id { get; set; }
        public int Beds { get; set; }
        public int IdPrison { get; set; }
        public int IdCellType { get; set; }

        [ForeignKey("IdPrison")]
        public virtual Prison Prison { get; set; }
        [ForeignKey("IdCellType")]
        public virtual CellType CellType { get; set; }
        public virtual ICollection<Prisoner> Prisoner { get; set; }

    }
}
