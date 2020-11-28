using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Repositories
{
    public interface ICellRepository
    {
        Cell SelectedCell(int id);
        Task<IEnumerable<Cell>> AllCell(int id);
        bool SaveChanges();
        void CreateCell(Cell cell);
        void DeleteCell(Cell cell);
        void UpdateCell(Cell cell);

    }
}
