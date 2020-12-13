using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class IsolationController : Controller
    {
        private string controller = "Izolacje";
        private readonly IIsolationService _isolationService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public IsolationController(IIsolationService isolationService, IMapper mapper, ILoggerService loggerService)
        {
            _isolationService = isolationService;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        [HttpGet("{id}")]
        public ActionResult<PassVM> SelectedIsolation(int id)
        {
            var isolation = _isolationService.SelectedIsolation(id);
            return Ok(_mapper.Map<IsolationVM>(isolation));
        }
        [HttpGet]
        public async Task<IEnumerable<Isolation>> AllIsolations()
        {
            string userName = User.Identity.Name;

            var isolations = await _isolationService.AllIsolations(userName);
            return isolations;
        }
        [HttpPost]
        public ActionResult<IsolationVM> AddPass(IsolationDTO isolationDTO)
        {
            string userName = User.Identity.Name;
            var isolationModel = _mapper.Map<Isolation>(isolationDTO);
            if (isolationModel == null)
            {
                return NotFound();
            }
            _isolationService.CreateIsolation(isolationModel);
            _isolationService.SetPrisonerStatusTrue(isolationModel);
            _isolationService.SaveChanges();
            _loggerService.AddLog(controller, "Dodano więźnia do izolatki", userName);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteIsolation(int id)
        {
            string userName = User.Identity.Name;

            var isolation = _isolationService.SelectedIsolation(id);
            if (isolation == null)
            {
                return NotFound();
            }

            _isolationService.DeleteIsolation(isolation);
            _isolationService.SetPrisonerStatusFalse(isolation);


            _isolationService.SaveChanges();
            _loggerService.AddLog(controller, "Usunięto więźnia z izolatki", userName);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateIsolation(int id, IsolationDTO isolationDTO)
        {
            string userName = User.Identity.Name;
            var isolation = _isolationService.SelectedIsolation(id);
            if (isolation == null)
            {
                return NotFound();
            }
            _mapper.Map(isolationDTO, isolation);
            _isolationService.UpdateIsolation(isolation);
            _isolationService.SaveChanges();
            _loggerService.AddLog(controller, "Edytowano izolację więźnia", userName);


            return Ok();
        }
    }
}
