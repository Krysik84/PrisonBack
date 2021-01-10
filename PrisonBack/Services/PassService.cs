using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using PrisonBack.Resources.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class PassService : IPassService
    {
        private readonly IPassRepository _passRepository;

        public PassService(IPassRepository passRepository)
        {
            _passRepository = passRepository;
        }
        public Task<IEnumerable<Pass>> AllPass(string userName)
        {
            return _passRepository.AllPass(userName);
        }

        public void CreatePass(Pass pass)
        {
            _passRepository.CreatePass(pass);
        }

        public void DeletePass(Pass pass)
        {
            _passRepository.DeletePass(pass);
        }

        public bool SaveChanges()
        {
            return _passRepository.SaveChanges();
        }

        public Pass SelectedPass(int id)
        {
            return _passRepository.SelectedPass(id);
        }


        public void SetPrisonerStatusFalse(Pass pass)
        {
            _passRepository.SetPrisonerStatusFalse(pass);
        }

        public void SetPrisonerStatusTrue(Pass pass)
        {
            _passRepository.SetPrisonerStatusTrue(pass);
        }

        public void UpdatePass(Pass pass)
        {
            _passRepository.UpdatePass(pass);
        }
    }
}
