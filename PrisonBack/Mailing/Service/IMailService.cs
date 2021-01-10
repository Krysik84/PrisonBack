using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string userName);
    }
}
