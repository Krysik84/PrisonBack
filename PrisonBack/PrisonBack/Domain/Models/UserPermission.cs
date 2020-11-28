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

        public int IdUser { get; set; }
        public int IdPermission { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        [ForeignKey("IdPermission")]
        public virtual Permission Permission { get; set; }

    }
}
