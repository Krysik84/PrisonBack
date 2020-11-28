using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("CellType")]
    public class CellType
    {
        [Key]
        public int Id { get; set; }
        public string CellName { get; set; }
    }
}
