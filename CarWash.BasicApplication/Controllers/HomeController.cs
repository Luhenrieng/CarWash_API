using AutoMapper;
using BasicDDD.Application;
using BasicDDD.Application.Interface;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BasicDDD.BasicApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientAppService _clientApp;

        public HomeController(IClientAppService clientApp)
        {
            _clientApp = clientApp;
        }

        public ActionResult Index()
        {
            
            Client client1 = _clientApp.GetAll().FirstOrDefault();
            ClientViewModel list2 = Mapper.Map<ClientViewModel>(client1);


            IEnumerable<Client> clients = _clientApp.GetAll();
            List<ClientViewModel> list = Mapper.Map<List<ClientViewModel>>(clients);
            //var clientViewModel = _clientApp.GetAll();

            return View();
        }
    }
}
