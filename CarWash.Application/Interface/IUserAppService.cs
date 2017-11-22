using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Application.Interface
{
    public interface IUserAppService
    {
        int Add(Domain.Entities.User user);

        List<Domain.Entities.User> List();
    }
}
