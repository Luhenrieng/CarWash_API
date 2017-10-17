using AutoMapper;
using BasicDDD.Application;
using BasicDDD.Application.Interface;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicDDD.BasicApplication.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientAppService _clientApp;

        public ClientController(IClientAppService clientApp)
        {
            _clientApp = clientApp;
        }

        public ActionResult Index()
        {
            var clientViewModel = Mapper.Map<List<Client>, List<ClientViewModel>>(_clientApp.GetAll());

            return View();
        }
    }
}
