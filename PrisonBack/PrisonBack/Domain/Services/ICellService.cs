using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface ICellService
    {
        Cell SelectedCell(int id);
        Task<IEnumerable<Cell>> AllCell(int id);
        void CreateCell(Cell cell);
        bool SaveChanges();
        void DeleteCell(Cell cell);
        void UpdateCell(Cell cell);

    }
}
