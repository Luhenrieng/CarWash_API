using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        List<Entities.Service> ListAllServices();
        int AddServiceToWasher(ServicesXUser serviceXUser);
    }
}
