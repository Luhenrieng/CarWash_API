﻿using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAll();
    }
}
