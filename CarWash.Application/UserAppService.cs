using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Service;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Application
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            this._userService = userService;
        }

        public string Add(User user)
        {
            return this._userService.Add(user);
        }

        public List<User> List()
        {
            return this._userService.List();
        }

        public string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state)
        {
            return this._userService.GetLocationFromAddress( address,  number,  neighborhood,  city,  state);
        }

        public string ValidateUser(User user)
        {
            return this._userService.ValidateUser(user);
        }

        public User GetByLogin(string email, string password)
        {
            return this._userService.GetByLogin(email, password);
        }

        public User GetByEmail(string email)
        {
            return this._userService.GetByEmail(email);
        }

        public User GetByToken(string token)
        {
            return this._userService.GetByToken(token);
        }

        public List<ServiceDescription> ListService(int userId)
        {
            return this._userService.ListService(userId);
        }

        public List<Washer> ListWasher()
        {
            return this._userService.ListWasher();
        }
    }
}
