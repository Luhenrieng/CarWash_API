using AutoMapper;
using BasicDDD.Application.Interface;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicDDD.BasicApplication.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {

        private readonly IClientAppService _clientApp;

        public ValuesController(IClientAppService clientApp)
        {
            _clientApp = clientApp;
        }

        // GET api/values
        public List<ClientViewModel> GetUsers()
        {
            IEnumerable<Client> clients = _clientApp.GetAll();
            List<ClientViewModel> list = Mapper.Map<List<ClientViewModel>>(clients);

            return list;
        }



        // GET api/values/5
        public ClientViewModel Get(int id)
        {
            Client client = _clientApp.GetAll().FirstOrDefault(c => c.Id == id);
            ClientViewModel clientViewModel = Mapper.Map<ClientViewModel>(client);
            return clientViewModel;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
