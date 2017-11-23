using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BasicDDD.Application.Interface;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
using BasicDDD.Application;

namespace BasicDDD.BasicApplication.Controllers
{
    [BasicAuthentication]
    public class ApiUserController : ApiController
    {

        private readonly IUserAppService _userAppService;

        public ApiUserController(IUserAppService userAppService)
        {
            this._userAppService = userAppService;
        }

        // GET: api/ApiUser
        public List<UserViewModel> Get()
        {
            List<Domain.Entities.User> users =  this._userAppService.List();
            List<UserViewModel> listUser = Mapper.Map<List<UserViewModel>>(users);
            return listUser;
        }

        // GET: api/ApiUser/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiUser
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiUser/5
        public void Delete(int id)
        {
        }
    }
}
