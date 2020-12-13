using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class PrisonerService : IPrisonerService
    {
        private readonly IPrisonerRepository _prisonerRepository;

        public PrisonerService(IPrisonerRepository prisonerRepository)
        {
            _prisonerRepository = prisonerRepository;
        }
        public async Task<IEnumerable<Prisoner>> AllPrisoner(string userName)
        {
            return await _prisonerRepository.AllPrisoner(userName);
        }

        public void CreatePrisoner(Prisoner prisoner)
        {
            _prisonerRepository.CreatePrisoner(prisoner);
        }

        public void DeletePrisoner(Prisoner prisoner)
        {
            _prisonerRepository.DeletePrisoner(prisoner);
        }

        public bool SaveChanges()
        {
            return _prisonerRepository.SaveChanges();
        }

        public Prisoner SelectedPrisoner(int id)
        {
            return _prisonerRepository.SelectedPrisoner(id);
        }

        public void UpdatePrisoner(Prisoner prisoner)
        {
            _prisonerRepository.UpdatePrisoner(prisoner);
        }
    }
}
