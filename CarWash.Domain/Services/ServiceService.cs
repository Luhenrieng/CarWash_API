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

namespace BasicDDD.Domain.Services
{
    public class ServiceService : IServiceService
    {
        private readonly Interfaces.Repositories.IServiceRepository _serviceRepository;

        public ServiceService(Interfaces.Repositories.IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }

        public List<Service> ListWashers(string token, int maxRadius, string latitude, string longitude)
        {
            return this._serviceRepository.ListAllServices();
        }

        public List<Service> ListAllServices()
        {
            return this._serviceRepository.ListAllServices();
        }

        public int AddServiceToWasher(ServicesXUser serviceXUser)
        {
            return this._serviceRepository.AddServiceToWasher(serviceXUser);
        }
    }
}
