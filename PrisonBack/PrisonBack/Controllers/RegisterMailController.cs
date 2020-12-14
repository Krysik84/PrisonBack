using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Mailing;
using PrisonBack.Mailing.Service;

namespace PrisonBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterMailController : Controller
    {
        private readonly IMailService _mailService;
        public RegisterMailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
