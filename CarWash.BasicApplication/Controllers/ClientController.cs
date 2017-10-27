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
        private readonly ILogAppService _logApp;

        //public ClientController(IClientAppService clientApp)
        //{
        //    _clientApp = clientApp;
        //}

        public ClientController(ILogAppService logApp)
        {
            _logApp = logApp;
        }

        public ActionResult Index()
        {
            //var clientViewModel = Mapper.Map<List<Client>, List<ClientViewModel>>(_clientApp.GetAll());

            Log log = new Log();
            log.Inserted = DateTime.Now;
            log.UserId = 1;
            log.Description = "Description1";
            log.OperationType = "Add Log";

            int id = _logApp.Add(log);

            return View();
        }
    }
}
