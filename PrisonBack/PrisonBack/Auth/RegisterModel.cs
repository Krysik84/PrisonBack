using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Auth
{

        public class RegisterModel
        {
            [Required(ErrorMessage = "Login jest wymagany")]
            public string UserName { get; set; }

            [EmailAddress]
            [Required(ErrorMessage = "Email jest wymagany")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Imie jest wymagane")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Nazwisko jest wymagane")]
            public string Forname { get; set; }
            [Required(ErrorMessage = "Kod zaproszenia jest wymagane")]
            public string InviteCode { get; set; }



    }

    }
