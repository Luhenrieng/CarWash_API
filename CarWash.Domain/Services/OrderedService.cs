using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Services
{
    public class OrderedService : IOrderedService
    {
        private readonly IOrderedRepository _orderedRepository;

        public OrderedService(IOrderedRepository orderedRepository)
        {
            this._orderedRepository = orderedRepository;
        }

        public int Add(Ordered ordered)
        {
            return _orderedRepository.Add(ordered);
        }
    }
}
