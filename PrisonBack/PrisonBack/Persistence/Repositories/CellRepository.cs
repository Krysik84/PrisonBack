using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrisonBack.Persistence.Repositories
{
    public class CellRepository : BaseRepository, ICellRepository
    {
        public CellRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Cell>> AllCell(string userName)
        {
            var prison = _context.UserPermissions.FirstOrDefault(x => x.UserName == userName);
            return await _context.Cells.Include(t => t.CellType).Where(x => x.IdPrison == prison.IdPrison).ToListAsync();
        }
        public Cell SelectedCell(int id)
        {
            return _context.Cells.Include(p => p.Prison).Include(t => t.CellType).FirstOrDefault(x => x.Id == id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void CreateCell(Cell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));

            }
            _context.Cells.Add(cell);
        }
        public void DeleteCell(Cell cell)
        {
            _context.Cells.Remove(cell);
        }

        public void UpdateCell(Cell cell)
        {
        }
    }
}
