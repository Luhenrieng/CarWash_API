using BasicDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Interfaces.Repositories;
using BasicDDD.Domain.Interfaces.Service;

namespace BasicDDD.Application
{
    public class LogAppService : ILogAppService
    {
        private readonly ILogService _logService;

        public LogAppService(ILogService logService)
        {
            _logService = logService;
        }

        public int Add(Log log)
        {
            return _logService.Add(log);
        }
    }
}
