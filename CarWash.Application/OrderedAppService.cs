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
    public class OrderedAppService : IOrderedAppService
    {
        private readonly IOrderedService _orderedService;

        public OrderedAppService(IOrderedService orderedService)
        {
            this._orderedService = orderedService;
        }

        public int Add(Ordered ordered)
        {
            return this._orderedService.Add(ordered);
        }

        public bool CreateOrder(Domain.Entities.ValueObjects.CreateOrder order)
        {
            return _orderedService.CreateOrder(order);
        }
    }
}
