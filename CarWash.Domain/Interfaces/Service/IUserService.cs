﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Service
{
    public interface IUserService
    {
        int Add(Entities.User user);

        List<Entities.User> List();

        string GetLocationFromAddress(string address, string number, string neighborhood, string city, string state);
    }
}
