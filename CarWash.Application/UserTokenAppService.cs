using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Service;

namespace BasicDDD.Application
{
    public class UserTokenAppService : IUserTokenAppService
    {
        private readonly IUserTokenService _userTokenService;

        public UserTokenAppService(IUserTokenService userTokenService)
        {
            this._userTokenService = userTokenService;
        }

        public string Add(UserToken userToken)
        {
            return this._userTokenService.Add(userToken);
        }
    }
}
