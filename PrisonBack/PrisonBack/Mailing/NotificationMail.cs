using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing
{
    public class NotificationMail
    {


        public string Body()
        {

            return "Testowy mail";
        }
        public string Title()
        {
            return "Notyfikacja";
        }
        public string To()
        {
            return "";
        }
    }
}
