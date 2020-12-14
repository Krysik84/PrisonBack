using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Resources.DTOs
{
    public class PrisonerDTO
    {
        [Required(ErrorMessage = "Proszę podać imię więźnia")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwisko więźnia")]
        public string Forname { get; set; }
        [Required(ErrorMessage = "Proszę podać pesel więźnia")]
        public string Pesel { get; set; }
        [Required(ErrorMessage = "Proszę podać adres więźnia")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Proszę zaznaczyć czy więzień jest na przepustce")]
        public bool Pass { get; set; }
        [Required(ErrorMessage = "Proszę wpisać ocenę zachowania więźnia")]
        public int Behavior { get; set; }
        [Required(ErrorMessage = "Proszę zaznaczyć czy więzień wymaga izolacji")]
        public bool Isolated { get; set; }
        [Required(ErrorMessage = "Proszę wybrać celę dla więźnia")]
        public int IdCell { get; set; }
    }
}
