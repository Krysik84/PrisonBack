using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrisonBack.Services
{
    public class PunishmentService : IPunishmentService
    {
        private readonly IPunishmentRepository _punishmentRepository;
        public PunishmentService(IPunishmentRepository punishmentRepository)
        {
            _punishmentRepository = punishmentRepository;
        }

        public void CreatePunishment(Punishment punishment)
        {
            _punishmentRepository.CreatePunishment(punishment);
        }

        public void DeletePunishment(Punishment punishment)
        {
            _punishmentRepository.DeletePunishment(punishment);
        }

        public bool SaveChanges()
        {
            return _punishmentRepository.SaveChanges();
        }

        public Punishment SelectedPunishment(int id)
        {
            return _punishmentRepository.SelectedPunishment(id);
        }

        public Punishment SelectedToDelateOrUpdatePunishment(int id)
        {
            return _punishmentRepository.SelectedToDelateOrUpdatePunishment(id);
        }

        public void UpdatePunishment(Punishment punishment)
        {
            _punishmentRepository.UpdatePunishment(punishment);
        }
    }
}
