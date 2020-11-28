using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrisonBack.Resources;
using Microsoft.AspNetCore.Mvc;
using System;


namespace PrisonBack.Persistence.Repositories
{
    public class PunishmentRepository : BaseRepository, IPunishmentRepository
    {
        public PunishmentRepository(AppDbContext context) : base(context)
        {

        }

        public void CreatePunishment(Punishment punishment)
        {
            if (punishment == null)
            {
                throw new ArgumentNullException(nameof(punishment));

            }
            _context.Punishments.Add(punishment);
        }

        public void DeletePunishment(Punishment punishment)
        {
            _context.Punishments.Remove(punishment);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);

        }

        public Punishment SelectedPunishment(int id)
        {
            return _context.Punishments.FirstOrDefault(x => x.IdPrisoner == id);
        }

        public Punishment SelectedToDelateOrUpdatePunishment(int id)
        {
            return _context.Punishments.FirstOrDefault(x => x.Id == id);
        }

        public void UpdatePunishment(Punishment punishment)
        {
        }
    }
}
