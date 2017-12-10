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
        public ApiResponse Get()
        {
            try
            {
                List<Domain.Entities.User> users = this._userAppService.List();
                List<UserViewModel> listUser = Mapper.Map<List<UserViewModel>>(users);
                return new ApiResponse(true, listUser);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }

        // GET: api/ApiUser/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiUser
        public ApiResponse Post([FromBody]UserViewModel user)
        {
            string statusMessage = this._userAppService.Add(Mapper.Map<Domain.Entities.User>(user));

            if(statusMessage == "")
                return new ApiResponse(true, "Usuário cadastrado com sucesso.");
            else
                return new ApiResponse(false, statusMessage);
        }

        // PUT: api/ApiUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiUser/5
        public void Delete(int id)
        {
        }

        //public void Test()
        //{
        //    string number = "255";
        //    string address = "Francisco+Morato";
        //    string neighborhood = "Menck";
        //    string city = "Osasco";
        //    string state = "SP";

        //    string strLocation = this._userAppService.GetLocationFromAddress(address, number, neighborhood, city, state);
        //}
    }
    
}
