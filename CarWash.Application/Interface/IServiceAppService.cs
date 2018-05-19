using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Application.Interface
{
    public interface IServiceAppService
    {
        List<Domain.Entities.Service> ListAllServices();
        int AddServiceToWasher(ServicesXUser serviceXUser);
    }
}
