using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class IsolationService : IIsolationService
    {
        private readonly IIsolationRepository _isolationRepository;
        public IsolationService(IIsolationRepository isolationRepository)
        {
            _isolationRepository = isolationRepository;
        }

        public async Task<IEnumerable<Isolation>> AllIsolations(string userName)
        {
            return await _isolationRepository.AllIsolations(userName);
        }

        public void CreateIsolation(Isolation isolation)
        {
            _isolationRepository.CreateIsolation(isolation);
        }

        public void DeleteIsolation(Isolation isolation)
        {
            _isolationRepository.DeleteIsolation(isolation);
        }

        public bool SaveChanges()
        {
            return _isolationRepository.SaveChanges();
        }

        public Isolation SelectedIsolation(int id)
        {
            return _isolationRepository.SelectedIsolation(id);
        }

        public void SetPrisonerStatusFalse(Isolation isolation)
        {
            _isolationRepository.SetPrisonerStatusFalse(isolation);
        }

        public void SetPrisonerStatusTrue(Isolation isolation)
        {
            _isolationRepository.SetPrisonerStatusTrue(isolation);
        }

        public void UpdateIsolation(Isolation isolation)
        {
            _isolationRepository.UpdateIsolation(isolation);
        }
    }
}
