using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources.ViewModels
{
    public class UserPermissionVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int IdPrison { get; set; }
    }
}
