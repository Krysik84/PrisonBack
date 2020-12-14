using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Auth;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]
    [Authorize(Roles = UserRoles.Admin)]
    public class LoggerController : Controller
    {

        private readonly ILoggerService _loggerService;


        public LoggerController(ILoggerService loggerService)
        {

            _loggerService = loggerService;
        }
        [HttpGet]
        public async Task<IEnumerable<Logger>> AllLogs()
        {
            string userName = User.Identity.Name;
            var logs = await _loggerService.AllLogs(userName);
            return logs;
        }
    }
}
