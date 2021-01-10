using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources.DTOs
{
    public class PassDTO
    {
        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Data zakończenia jest wymagana")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Należy wybrać więźnia")]
        public int IdPrisoner { get; set; }
    }
}
