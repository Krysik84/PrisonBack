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
    public class PrisonerRepository : BaseRepository, IPrisonerRepository
    {
        public PrisonerRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Prisoner>> AllPrisoner(int id)
        {
            return await _context.Prisoners.Where(x => x.Cell.IdPrison == id).Include(x => x.Punishments).ToListAsync();
        }

        public void CreatePrisoner(Prisoner prisoner)
        {
            _context.Prisoners.Add(prisoner);
        }

        public void DeletePrisoner(Prisoner prisoner)
        {
            _context.Prisoners.Remove(prisoner);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Prisoner SelectedPrisoner(int id)
        {
            return _context.Prisoners.FirstOrDefault(x => x.Id == id);

        }

        public void UpdatePrisoner(Prisoner prisoner)
        {
        }
    }
}
