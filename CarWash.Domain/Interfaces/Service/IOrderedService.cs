﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Service
{
    public interface IOrderedService
    {
        int Add(Entities.Ordered ordered);
        bool CreateOrder(Entities.ValueObjects.CreateOrder order);
    }
}
