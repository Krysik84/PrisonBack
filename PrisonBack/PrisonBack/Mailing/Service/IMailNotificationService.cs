using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing.Service
{
    public interface IMailNotificationService
    {
        void SendEmailAsync(MailRequest mailRequest);

    }
}
