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
    public class ClientAppService : IClientAppService
    {
        private readonly IClientService _clientService;

        public ClientAppService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public List<Client> GetAll()
        {
            return _clientService.GetAll();
        }
    }
}
