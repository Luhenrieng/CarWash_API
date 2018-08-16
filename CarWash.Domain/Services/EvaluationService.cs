using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;

namespace BasicDDD.Domain.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _evaluationRepository;

        public EvaluationService(IEvaluationRepository evaluationRepository)
        {
            this._evaluationRepository = evaluationRepository;
        }

        public int Add(Evaluation evaluation)
        {
            return _evaluationRepository.Add(evaluation);
        }
    }
}
