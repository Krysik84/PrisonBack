using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("Logger")]
    public class Logger
    {
        [Key]
        public int Id { get; set; }
        public DateTime LogData { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int IdPrison { get; set; }

        [ForeignKey("IdPrison")]
        public virtual Prison Prison { get; set; }
    }
}
