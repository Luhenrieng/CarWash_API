using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;

namespace BasicDDD.Application.Interface
{
    public interface ILogAppService
    {
        int Add(Domain.Entities.Log log);
    }
}
