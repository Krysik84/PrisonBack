using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]

    public class PassController : Controller
    {
        private readonly IPassService _passService;
        private readonly IMapper _mapper;
        private string controller = "Przepustki";
        private readonly ILoggerService _loggerService;


        public PassController(IPassService passService, IMapper mapper, ILoggerService loggerService)
        {
            _passService = passService;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        [HttpGet("{id}")]
        public ActionResult<PassVM> SelectedPass(int id)
        {
            var pass = _passService.SelectedPass(id);
            return Ok(_mapper.Map<PassVM>(pass));
        }
        [HttpGet]
        public async Task<IEnumerable<Pass>> AllPasses()
        {
            string userName = User.Identity.Name;

            var pass = await _passService.AllPass(userName);
            return pass;
        }
        [HttpPost]
        public ActionResult<PassVM> AddPass(PassDTO passDTO)
        {
            string userName = User.Identity.Name;
            var passModel = _mapper.Map<Pass>(passDTO);
            if(passModel == null)
            {
                return NotFound();
            }
            _passService.CreatePass(passModel);
            _passService.SetPrisonerStatusTrue(passModel);
            _passService.SaveChanges();

            _loggerService.AddLog(controller, "Dodano przepustkę więźnia", userName);

            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePass(int id)
        {
            string userName = User.Identity.Name;
            var pass = _passService.SelectedPass(id);
            if (pass == null)
            {
                return NotFound();
            }

            _passService.DeletePass(pass);
            _passService.SetPrisonerStatusFalse(pass);


            _passService.SaveChanges();
            _loggerService.AddLog(controller, "Usunięto przepustkę więźnia", userName);

            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdatePass(int id, PassDTO passDTO)
        {
            string userName = User.Identity.Name;
            var pass = _passService.SelectedPass(id);
            if (pass == null)
            {
                return NotFound();
            }
            _mapper.Map(passDTO, pass);
            _passService.UpdatePass(pass);
            _passService.SaveChanges();
            _loggerService.AddLog(controller, "Edytowano przepustkę więźnia", userName);


            return Ok();
        }

       
    }
}
