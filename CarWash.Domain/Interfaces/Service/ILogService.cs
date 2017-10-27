using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Domain.Interfaces.Service
{
    public interface ILogService
    {
        int Add(Entities.Log log);
    }
}
