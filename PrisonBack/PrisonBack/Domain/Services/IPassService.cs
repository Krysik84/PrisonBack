using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface IPassService
    {
        Pass SelectedPass(int id);
        Task<IEnumerable<Pass>> AllPass(int id);
        bool SaveChanges();
        void CreatePass(Pass pass);
        void DeletePass(Pass pass);
        void UpdatePass(Pass pass);
        void SetPrisonerStatusTrue(Pass pass);
        void SetPrisonerStatusFalse(Pass pass);
    }
}
