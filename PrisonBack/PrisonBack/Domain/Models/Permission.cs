using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonBack.Domain.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
