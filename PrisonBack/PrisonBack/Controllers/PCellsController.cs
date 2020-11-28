using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using PrisonBack.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonBack.Controllers
{
    [Route("/api/[controller]")]

    public class PCellsController : Controller
    {
        private readonly ICellService _cellService;
        private readonly IMapper _mapper;

        public PCellsController(ICellService cellService, IMapper mapper)
        {
            _cellService = cellService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CellVM> SelectedCell(int id)
        {
            var cell = _cellService.SelectedCell(id);
            return Ok(_mapper.Map<CellVM>(cell));
        }
        [HttpGet]
        public async Task<IEnumerable<Cell>> AllCell(int id)
        {
            var cell = await _cellService.AllCell(id);
            return cell;
        }
        [HttpPost]
        public ActionResult<CellVM> AddCell(CellDTO cellDTO)
        {
            var cellModel = _mapper.Map<Cell>(cellDTO);
            _cellService.CreateCell(cellModel);
            _cellService.SaveChanges();



            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCell(int id)
        {
            var cell = _cellService.SelectedCell(id);
            if(cell == null)
            {
                return NotFound();
            }
            _cellService.DeleteCell(cell);
            _cellService.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCell(int id, CellDTO cellDTO)
        {
            var cell = _cellService.SelectedCell(id);
            if(cell == null)
            {
                return NotFound();
            }
            _mapper.Map(cellDTO, cell);
            _cellService.UpdateCell(cell);
            _cellService.SaveChanges();


            return NoContent();
        }

    }
}
