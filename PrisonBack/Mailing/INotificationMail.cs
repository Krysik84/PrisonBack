using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing
{
    public interface INotificationMail
    {
        string Body(string userName);
        string To(string email);
        string Title();
    }
}
