using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Service;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Application
{
    public class ServiceAppService : IServiceAppService
    {
        private readonly IServiceService _serviceService;

        public ServiceAppService(IServiceService serviceService)
        {
            this._serviceService = serviceService;
        }

        public int AddServiceToWasher(ServicesXUser serviceXUser)
        {
            return this._serviceService.AddServiceToWasher(serviceXUser);
        }

        public List<Service> ListAllServices()
        {
            return this._serviceService.ListAllServices();
        }
    }
}
