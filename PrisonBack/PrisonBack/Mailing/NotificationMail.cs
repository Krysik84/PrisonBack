using PrisonBack.Domain.Repositories;
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
            return "PrisonBreak <br /> Dzisiaj jest " + DateTime.Now+ "<br /> Życzymi miłej pracy <br /> Team PrisonBreak";
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
