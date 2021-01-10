using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class CellService : ICellService
    {
        private readonly ICellRepository _cellRepository;

        public CellService(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }
        public Cell SelectedCell(int id)
        {
            return _cellRepository.SelectedCell(id);
        }
        public async Task<IEnumerable<Cell>> AllCell(String userName)
        {
            return await _cellRepository.AllCell(userName);
        }
        public void CreateCell(Cell cell)
        {
            _cellRepository.CreateCell(cell);
        }
        public bool SaveChanges()
        {
            return _cellRepository.SaveChanges();
        }
        public void DeleteCell(Cell cell)
        {
            _cellRepository.DeleteCell(cell);
        }

        public void UpdateCell(Cell cell)
        {
            _cellRepository.UpdateCell(cell);
        }

        public int PrisonID(string userName)
        {
            return _cellRepository.PrisonID(userName);
        }
    }
}
