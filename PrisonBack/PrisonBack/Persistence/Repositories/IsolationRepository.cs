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
    public class IsolationRepository : BaseRepository, IIsolationRepository
    {
        public IsolationRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Isolation>> AllIsolations(string userName)
        {
            var prison = _context.UserPermissions.FirstOrDefault(x => x.UserName == userName);
            return await _context.Isolations.Where(x => x.Prisoner.Cell.Prison.Id == prison.Id).ToListAsync(); ;
        }

        public void CreateIsolation(Isolation isolation)
        {
            _context.Add(isolation);
        }

        public void DeleteIsolation(Isolation isolation)
        {
            _context.Remove(isolation);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Isolation SelectedIsolation(int id)
        {
           return _context.Isolations.FirstOrDefault(x => x.Id == id);
        }

        public void SetPrisonerStatusFalse(Isolation isolation)
        {
            Prisoner prisoner = _context.Prisoners.FirstOrDefault(x => x.Id == isolation.IdPrisoner);
            prisoner.Pass = false;
        }

        public void SetPrisonerStatusTrue(Isolation isolation)
        {
            Prisoner prisoner = _context.Prisoners.FirstOrDefault(x => x.Id == isolation.IdPrisoner);
            prisoner.Pass = true;
        }

        public void UpdateIsolation(Isolation isolation)
        {
        }
    }
}
