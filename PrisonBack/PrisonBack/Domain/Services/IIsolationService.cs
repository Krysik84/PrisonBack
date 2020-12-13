using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface IIsolationService
    {
        Isolation SelectedIsolation(int id);
        Task<IEnumerable<Isolation>> AllIsolations(string userName);
        bool SaveChanges();
        void CreateIsolation(Isolation isolation);
        void DeleteIsolation(Isolation isolation);
        void UpdateIsolation(Isolation isolation);
        void SetPrisonerStatusTrue(Isolation isolation);
        void SetPrisonerStatusFalse(Isolation isolation);
    }
}
