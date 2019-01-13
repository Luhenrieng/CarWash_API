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
using BasicDDD.Domain.Entities.ValueObjects;
using System.Text.RegularExpressions;

namespace BasicDDD.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly Interfaces.Repositories.IUserRepository _userRepository;

        public List<User> ListWashers(string token, int maxRadius, string latitude, string longitude)
        {
            return this._userRepository.List();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(Interfaces.Repositories.IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// List Method
        /// </summary>
        /// <returns></returns>
        public List<User> List()
        {
            return this._userRepository.List();
        }

        /// <summary>
        /// Get GeoLocation
        /// </summary>
        /// <param name="address"></param>
        /// <param name="number"></param>
        /// <param name="neighborhood"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state)
        {
            GoogleGeoCoding geoCoding = new GoogleGeoCoding();
            string str = geoCoding.GetLocationFromAddress( address,  number,  neighborhood,  city,  state);

            return str;
        }

        /// <summary>
        /// Get By Login Method
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetByLogin(string email, string password)
        {
            return this._userRepository.GetByLogin(email, Security.EncryptSHA512Managed(password));
        }

        /// <summary>
        /// Get By E-mail Method
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return this._userRepository.GetByEmail(email);
        }

        /// <summary>
        /// Get User By Token Method
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User GetByToken(string token)
        {
            return this._userRepository.GetByToken(token);
        }

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Add(User user)
        {
            try
            {
                string stateMessage = ValidateUser(user);
                if (stateMessage != "")
                    return stateMessage;

                User checkUser = this.GetByEmail(user.Email);
                if (checkUser != null)
                    return "E-mail já cadastrado.";

                string strLocation = GetLocationFromAddress(user.Address, user.AddressNumber.ToString(), user.District, user.City, user.State);
                if (string.IsNullOrEmpty(strLocation))
                    return "A geolocalização não pode ser vinculada, por favor verifique o endereço e tente novamente.";

                user.Inserted = DateTime.Now;
                user.Active = true;
                user.Latitude = strLocation.Split('&')[0];
                user.Longitude = strLocation.Split('&')[1];
                user.Password = Security.EncryptSHA512Managed(user.Password);

                int cod = this._userRepository.Add(user);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string ValidateUser(User user)
        {
            string pattern = @"^(?=.*[a-zA-Z])(?=.*[0-9]).+$";
            Regex regex = new Regex(pattern);

            if (string.IsNullOrEmpty(user.Name))
                return "Campo nome não pode ser nulo.";
            else if (string.IsNullOrEmpty(user.Email))
                return "Campo E-mail não pode ser nulo.";
            else if (user.RoleId < 0 || user.RoleId > 5)
                return "Tipo de usuário não pode ser nulo.";

            else if (string.IsNullOrEmpty(user.Password))
                return "Campo senha não pode ser nulo.";
            else if (user.Password.Count() < 6)
                return "Campo senha deve conter no mínimo 6 caracteres.";
            else if (user.Password.Count() > 10)
                return "Campo senha deve conter no máximo 10 caracteres.";
            else if (!regex.IsMatch(user.Password))
                return "Campo senha deve conter números e letras.";

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

        public List<ServiceDescription> ListService(int userId)
        {
            return this._userRepository.ListService(userId);
        }

        public List<Washer> ListWasher()
        {
            return this._userRepository.ListWasher();
        }
    }
}
