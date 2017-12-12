﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasicDDD.Application.Interface
{
    public interface IUserAppService
    {
        string Add(Domain.Entities.User user);

        List<Domain.Entities.User> List();

        string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state);

        string ValidateUser(Domain.Entities.User user);

        Domain.Entities.User GetByLogin(string email, string password);

        Domain.Entities.User GetByEmail(string email);
    }
}
