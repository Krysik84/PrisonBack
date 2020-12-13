using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    public class InviteCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public int IdPrison { get; set; }
        [ForeignKey("IdPrison")]
        public virtual Prison Prison { get; set; }
    }
}
