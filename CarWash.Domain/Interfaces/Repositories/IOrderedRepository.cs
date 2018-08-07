using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Repositories
{
    public interface IOrderedRepository
    {
        int Add(Entities.Ordered ordered);
    }
}
