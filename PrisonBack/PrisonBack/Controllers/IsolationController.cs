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
        private readonly IIsolationService _isolationService;
        private readonly IMapper _mapper;

        public IsolationController(IIsolationService isolationService, IMapper mapper)
        {
            _isolationService = isolationService;
            _mapper = mapper;
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
            var isolationModel = _mapper.Map<Isolation>(isolationDTO);
            _isolationService.CreateIsolation(isolationModel);

            _isolationService.SetPrisonerStatusTrue(isolationModel);

            _isolationService.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteIsolation(int id)
        {
            var isolation = _isolationService.SelectedIsolation(id);
            if (isolation == null)
            {
                return NotFound();
            }

            _isolationService.DeleteIsolation(isolation);
            _isolationService.SetPrisonerStatusFalse(isolation);


            _isolationService.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateIsolation(int id, IsolationDTO isolationDTO)
        {
            var isolation = _isolationService.SelectedIsolation(id);
            if (isolation == null)
            {
                return NotFound();
            }
            _mapper.Map(isolationDTO, isolation);
            _isolationService.UpdateIsolation(isolation);
            _isolationService.SaveChanges();


            return NoContent();
        }
    }
}
