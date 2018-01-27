using BasicDDD.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        int Add(Entities.User user);

        List<Entities.User> List();

        Entities.User GetByLogin(string email, string password);

        Entities.User GetByEmail(string email);

        Entities.User GetByToken(string token);

        List<ServiceDescription> ListService(int userId);
    }
}
