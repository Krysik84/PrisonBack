using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int IdPrison { get; set; }
        [ForeignKey("IdPrison")]
        public virtual Prison Prison { get; set; }

    }
}
