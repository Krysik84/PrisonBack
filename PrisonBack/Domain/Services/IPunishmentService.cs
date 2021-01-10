using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface IPunishmentService
    {
        Punishment SelectedPunishment(int id);
        Punishment SelectedToDelateOrUpdatePunishment(int id);

        void CreatePunishment(Punishment punishment);
        bool SaveChanges();
        void DeletePunishment(Punishment punishment);
        void UpdatePunishment(Punishment punishment);

    }
}
