using BasicDDD.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Service
{
    public interface IUserService
    {
        string Add(Entities.User user);

        List<Entities.User> List();

        string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state);

        string ValidateUser(Entities.User user);

        Entities.User GetByLogin(string email, string password);

        Entities.User GetByEmail(string email);

        Entities.User GetByToken(string token);

        List<ServiceDescription> ListService(int userId);
    }
}
