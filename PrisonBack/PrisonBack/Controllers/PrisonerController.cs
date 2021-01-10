using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class PrisonerController : Controller
    {
        private readonly IPrisonerService _prisonerService;
        private readonly IMapper _mapper;

        public PrisonerController(IPrisonerService prisonerService, IMapper mapper)
        {
            _prisonerService = prisonerService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]


        public ActionResult<PrisonerVM> SelectedPrisoner(int id)
        {
            var prisoner = _prisonerService.SelectedPrisoner(id);
            return Ok(_mapper.Map<PrisonerVM>(prisoner));
        }
        [HttpGet]
        public async Task<IEnumerable<Prisoner>> AllPrisoner()
        { 
            string userName = User.Identity.Name;
            

            var prisoner = await _prisonerService.AllPrisoner(userName);
            return prisoner;
        }
        [HttpPost]
        public ActionResult<PrisonerVM> AddPrisoner(PrisonerDTO prisonerDTO)
        {
            var prisonerModel = _mapper.Map<Prisoner>(prisonerDTO);
            _prisonerService.CreatePrisoner(prisonerModel);
            _prisonerService.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePrisoner(int id)
        {
            var prisoner = _prisonerService.SelectedPrisoner(id);
            if (prisoner == null)
            {
                return NotFound();
            }
            _prisonerService.DeletePrisoner(prisoner);
            _prisonerService.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdatePrisoner(int id, PrisonerDTO prisonerDTO)
        {
            var prisoner = _prisonerService.SelectedPrisoner(id);
            if (prisoner == null)
            {
                return NotFound();
            }
            _mapper.Map(prisonerDTO, prisoner);
            _prisonerService.UpdatePrisoner(prisoner);
            _prisonerService.SaveChanges();


            return NoContent();
        }

    }
}
