using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Service;

namespace BasicDDD.Application
{
    public class EvaluationAppService : IEvaluationAppService
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationAppService(IEvaluationService evaluationService)
        {
            this._evaluationService = evaluationService;
        }

        public int Add(Evaluation evaluation)
        {
            return this._evaluationService.Add(evaluation);
        }
    }
}
