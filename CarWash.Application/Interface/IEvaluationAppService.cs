using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDDD.Application.Interface
{
    public interface IEvaluationAppService
    {
        int Add(Domain.Entities.Evaluation evaluation);
    }
}
