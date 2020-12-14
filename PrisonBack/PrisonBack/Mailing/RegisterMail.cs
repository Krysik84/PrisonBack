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
        public string Body()
        {

            return "PrisonBreak \n Witaj nowy użytkowniku \n Aby zarejestrować nowe konto postępuj zgodnie z instrukcją." +
                "\n1. Wejdź na adres localhost:blabla\n2. Wypełnij wszystkie pola swoimi danymi\n" +
                "3. Wpisz swój kod "+_inviteCodeService.CreateCode()+"\n4. Naciśnij zarejestruj\n5. Jeśli prawidłowo wypełniłeś wszystkie pola możesz przejść do logowania\n" +
                "Pozdrawiamy team PrisonBreak";
        }
        public string Title()
        {
            return "Rejestracja w systemie PrisonBreak";
        }
    }
}
