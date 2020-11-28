using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Forname { get; set; }
        [Required]
        [MaxLength(24), MinLength(6)]
        public string Login { get; set; }
        [Required]
        [MaxLength(24), MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Mail { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<Pass> Passes { get; set; }

    }
}
