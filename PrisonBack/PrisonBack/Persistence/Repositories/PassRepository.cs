using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Persistence.Repositories
{
    public class PassRepository : BaseRepository, IPassRepository
    {
        public PassRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pass>> AllPass(int id)
        {
            return await _context.Passes.Where(x => x.Prisoner.Cell.IdPrison == id).Include(x => x.Prisoner).ToListAsync();
        }

        public void CreatePass(Pass pass)
        {
            _context.Passes.Add(pass);
        }

        public void DeletePass(Pass pass)
        {
            _context.Passes.Remove(pass);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Pass SelectedPass(int id)
        {
            return _context.Passes.FirstOrDefault(x => x.Id == id);
        }

        public void SetPrisonerStatusFalse(Pass pass)
        {
            Prisoner prisoner = _context.Prisoners.FirstOrDefault(x => x.Id == pass.IdPrisoner);
            prisoner.Pass = false;
        }

        public void SetPrisonerStatusTrue(Pass pass)
        {
   
            Prisoner prisoner = _context.Prisoners.FirstOrDefault(x => x.Id == pass.IdPrisoner);
            prisoner.Pass = true;

        }

        public void UpdatePass(Pass pass)
        {
            
        }
    }
}
