using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;

namespace BasicDDD.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly Interfaces.Repositories.IUserRepository _userRepository;

        public UserService(Interfaces.Repositories.IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public int Add(User user)
        {
            return this._userRepository.Add(user);
        }

        public List<User> List()
        {
            return this._userRepository.List();
        }
    }
}
