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

        public PassController(IPassService passService, IMapper mapper)
        {
            _passService = passService;
            _mapper = mapper;
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
            var passModel = _mapper.Map<Pass>(passDTO);
            _passService.CreatePass(passModel);

            _passService.SetPrisonerStatusTrue(passModel);

            _passService.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePass(int id)
        {
            var pass = _passService.SelectedPass(id);
            if (pass == null)
            {
                return NotFound();
            }

            _passService.DeletePass(pass);
            _passService.SetPrisonerStatusFalse(pass);


            _passService.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdatePass(int id, PassDTO passDTO)
        {
            var pass = _passService.SelectedPass(id);
            if (pass == null)
            {
                return NotFound();
            }
            _mapper.Map(passDTO, pass);
            _passService.UpdatePass(pass);
            _passService.SaveChanges();


            return NoContent();
        }

       
    }
}
