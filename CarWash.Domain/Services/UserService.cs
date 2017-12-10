using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using System.Net;
using System.IO;
using CarWash.Util;

namespace BasicDDD.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly Interfaces.Repositories.IUserRepository _userRepository;

        public UserService(Interfaces.Repositories.IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public string Add(User user)
        {
            try
            {
                string stateMessage = ValidateUser(user);

                if (stateMessage != "")
                    return stateMessage;

                string strLocation = GetLocationFromAddress(user.Address, user.AddressNumber.ToString(), user.District, user.City, user.State);

                if (string.IsNullOrEmpty(strLocation))
                    return "A geolocalização não pode ser vinculada, por favor verifique o endereço e tente novamente.";

                user.Inserted = DateTime.Now;
                user.Active = true;
                user.GeoLocation = strLocation;

                int cod = this._userRepository.Add(user);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<User> List()
        {
            return this._userRepository.List();
        }

        public string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state)
        {
            GoogleGeoCoding geoCoding = new GoogleGeoCoding();
            string str = geoCoding.GetLocationFromAddress( address,  number,  neighborhood,  city,  state);

            return str;
        }

        public string ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                return "Campo nome não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.Email))
                return "Campo E-mail não pode ser nulo.";
            else if (user.RoleId < 0 || user.RoleId > 5)
                return "Tipo de usuário não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.Password))
                return "Campo senha não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.Document))
                return "Campo CPF não pode ser nulo.";
            else if (user.BirthDate == null || user.BirthDate < DateTime.Now.AddYears(-100))
                return "Campo data de nascimento inválido.";
            else if (string.IsNullOrEmpty(user.Cep))
                return "Campo Cep não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.Address))
                return "Campo endereço não pode ser nulo.";
            else if (user.AddressNumber <= 0)
                return "Campo número não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.District))
                return "Campo bairro não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.City))
                return "Campo cidade não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.State))
                return "Campo Estado não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.PhoneNumber))
                return "Campo telefone não pode ser nulo.";

            return "";
        }
    }
}
