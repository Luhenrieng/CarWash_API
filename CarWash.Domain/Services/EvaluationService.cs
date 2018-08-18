using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using BasicDDD.Domain.Entities.ValueObjects;

namespace BasicDDD.Domain.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _evaluationRepository;

        public EvaluationService(IEvaluationRepository evaluationRepository)
        {
            this._evaluationRepository = evaluationRepository;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="eval"></param>
        /// <returns></returns>
        public string Add(EvaluateUser eval)
        {
            try
            {
                string validate = Validate(eval);

                if (validate == "")
                {
                    Evaluation evaluation = new Evaluation();
                    evaluation.Date = DateTime.Now;
                    evaluation.OrderedId = eval.OrderedId;
                    evaluation.Score = eval.Score;
                    evaluation.UserIdFrom = eval.UserIdFrom;
                    evaluation.UserIdTo = eval.UserIdTo;
                    _evaluationRepository.Add(evaluation);
                }

                return validate;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="evaluate"></param>
        /// <returns></returns>
        private string Validate(EvaluateUser evaluate)
        {
            if (evaluate.OrderedId == 0)
                return "Número de pedido inválido.";
            if (evaluate.UserIdFrom == 0)
                return "Usuário avaliador inválido.";
            if (evaluate.UserIdTo == 0)
                return "Usuário avaliado inválido.";
            if (evaluate.Score > 5 || evaluate.Score < 0)
                return "A nota deve estar entre 0 e 5.";

            return "";
        }
    }
}
