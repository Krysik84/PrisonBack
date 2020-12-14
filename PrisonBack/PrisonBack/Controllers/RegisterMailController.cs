using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Auth;
using PrisonBack.Mailing;
using PrisonBack.Mailing.Service;

namespace PrisonBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
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
                string userName = User.Identity.Name;
                await _mailService.SendEmailAsync(request, userName);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
