using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class AddUserService : IAddUserService
    {
        private readonly IAddUserRepository _addUserRepository;

        public AddUserService(IAddUserRepository addUserRepository)
        {
            _addUserRepository = addUserRepository;
        }
        public void AddUserToPrison(string code, string userName)
        {
            _addUserRepository.AddUserToPrison(code, userName);
        }
    }
}
