using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources
{
    public class PunishmentDTO
    {
        [Required(ErrorMessage = "Proszę wybrać więźnia")]
        public int IdPrisoner { get; set; }
        [Required(ErrorMessage = "Proszę wybrać powód odbywania kary")]

        public int IdReason { get; set; }
        [Required(ErrorMessage = "Proszę zaznaczyć czy więznień odbywa dożywocie")]

        public bool Lifery { get; set; }
        [Required(ErrorMessage = "Data rozpoczęcia odbywania kary jest wymagana")]

        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Data zakończenia odbywania kary jest wymagana")]

        public DateTime EndDate { get; set; }
    }
}
