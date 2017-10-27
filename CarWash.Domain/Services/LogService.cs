using BasicDDD.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;

namespace BasicDDD.Domain.Services
{
    public class LogService : ILogService
    {
        private readonly Interfaces.Repositories.ILogRepository _logRepository;

        public LogService(Interfaces.Repositories.ILogRepository logRepository)
        {
            this._logRepository = logRepository;
        }

        public int Add(Log log)
        {
            return _logRepository.Add(log);
        }
    }
}
