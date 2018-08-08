using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Application.Interface
{
    public interface IOrderedAppService
    {
        int Add(Domain.Entities.Ordered ordered);
        bool CreateOrder(Domain.Entities.ValueObjects.CreateOrder order);
    }
}
