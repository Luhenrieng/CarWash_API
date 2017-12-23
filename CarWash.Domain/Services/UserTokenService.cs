using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using CarWash.Util;

namespace BasicDDD.Domain.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(IUserTokenRepository userTokenRepository)
        {
            this._userTokenRepository = userTokenRepository;
        }

        public string Add(UserToken userToken)
        {
            try
            {
                Random r = new Random();
                int randomNum = r.Next(9999999);
                string token = Security.EncryptSHA512Managed(randomNum.ToString());
                userToken.Token = token;

                int num = this._userTokenRepository.Add(userToken);
                return token;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
