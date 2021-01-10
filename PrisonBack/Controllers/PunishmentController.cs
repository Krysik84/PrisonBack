using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using PrisonBack.Resources;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]

    public class PunishmentController : Controller
    {
        private readonly IPunishmentService _punishmentService;
        private readonly IMapper _mapper;
        private string controller = "Kary";
        private readonly ILoggerService _loggerService;
        public PunishmentController(IPunishmentService punishmentService, IMapper mapper, ILoggerService loggerService)
        {
            _punishmentService = punishmentService;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        [HttpGet("{id}")]
        public ActionResult<Punishment> SelectedPunishment(int id)
        {
            var punishment = _punishmentService.SelectedPunishment(id);
            return Ok(_mapper.Map<PunishmentVM>(punishment));
        }
        [HttpPost]
        public ActionResult<Punishment> AddPunishment(PunishmentDTO punishmentDTO)
        {
            string userName = User.Identity.Name;

            var punishmentModel = _mapper.Map<Punishment>(punishmentDTO);
            if(punishmentModel == null)
            {
                return NotFound();
            }
            _punishmentService.CreatePunishment(punishmentModel);
            _punishmentService.SaveChanges();
            _loggerService.AddLog(controller, "Dodano karę dla więźnia", userName);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePunishment(int id)
        {
            string userName = User.Identity.Name;

            var punishment = _punishmentService.SelectedToDelateOrUpdatePunishment(id);
            if (punishment == null)
            {
                return NotFound();
            }
            _punishmentService.DeletePunishment(punishment);
            _punishmentService.SaveChanges();
            _loggerService.AddLog(controller, "Usunięto karę więźnia", userName);

            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdatePunishment(int id, PunishmentDTO punishmentDTO)
        {
            string userName = User.Identity.Name;

            var punishment = _punishmentService.SelectedToDelateOrUpdatePunishment(id);
            if (punishment == null)
            {
                return NotFound();
            }
            _mapper.Map(punishmentDTO, punishment);
            _punishmentService.UpdatePunishment(punishment);
            _punishmentService.SaveChanges();
            _loggerService.AddLog(controller, "Edytowano karę więźnia", userName);



            return Ok();
        }


    }
}
