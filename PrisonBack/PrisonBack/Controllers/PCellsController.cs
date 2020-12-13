using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Auth;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using PrisonBack.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]

    public class PCellsController : Controller
    {
        private string controller = "Cele";
        private readonly ICellService _cellService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

       public PCellsController(ICellService cellService, IMapper mapper, ILoggerService loggerService)
        {
            _cellService = cellService;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        [HttpGet("{id}")]
        public ActionResult<CellVM> SelectedCell(int id)
        {
            var cell = _cellService.SelectedCell(id);
            return Ok(_mapper.Map<CellVM>(cell));
        }
        [HttpGet]
        public async Task<IEnumerable<Cell>> AllCell()
        {
            string userName = User.Identity.Name;
            var cell = await _cellService.AllCell(userName);
            return cell;
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult<CellVM> AddCell(CellDTO cellDTO)
        {
            string userName = User.Identity.Name;

            var cellModel = _mapper.Map<Cell>(cellDTO);
            cellModel.IdPrison = _cellService.PrisonID(userName);
            if (cellModel == null)
            {
                return NotFound();
            }
            _cellService.CreateCell(cellModel);
            _cellService.SaveChanges();
            _loggerService.AddLog(controller, "Dodano nową cele", userName);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult DeleteCell(int id)
        {
            string userName = User.Identity.Name;
            var cell = _cellService.SelectedCell(id);
            if(cell == null)
            {
                return NotFound();
            }
            _cellService.DeleteCell(cell);
            _cellService.SaveChanges();
            _loggerService.AddLog(controller, "Usunięto cele o ID " + cell.Id, userName);
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult UpdateCell(int id, CellDTO cellDTO)
        {
            string userName = User.Identity.Name;
            var cell = _cellService.SelectedCell(id);
            if(cell == null)
            {
                return NotFound();
            }
            _mapper.Map(cellDTO, cell);
            _cellService.UpdateCell(cell);
            _cellService.SaveChanges();

            _loggerService.AddLog(controller, "Edytowano cele o ID " + cell.Id, userName);

            return Ok();
        }

    }
}
