using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing
{
    public class RegisterMail
    {
        private readonly IInviteCodeService _inviteCodeService;

        public RegisterMail(IInviteCodeService inviteCodeService)
        {
            _inviteCodeService = inviteCodeService;
        }
        public string Body(string userName)
        {

            return "PrisonBreak < br /> Witaj nowy użytkowniku <br /> Aby zarejestrować nowe konto postępuj zgodnie z instrukcją." +
                " <br /> 1. Wejdź na adres localhost:blabla <br /> 2. Wypełnij wszystkie pola swoimi danymi<br />" +
                "3. Wpisz swój kod " + _inviteCodeService.CreateCode(userName) + " <br /> 4. Naciśnij zarejestruj <br /> 5. Jeśli prawidłowo wypełniłeś wszystkie pola możesz przejść do logowania <br /> " +
                "Pozdrawiamy team PrisonBreak"; 
        }
        public string Title()
        {
            return "Rejestracja w systemie PrisonBreak";
        }
    }
}
