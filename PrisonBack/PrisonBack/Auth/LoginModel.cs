using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }
    }
}
