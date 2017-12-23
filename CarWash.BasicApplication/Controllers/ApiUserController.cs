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
    [RoutePrefix("api/ApiUser")]
    public class ApiUserController : ApiController
    {

        private readonly IUserAppService _userAppService;
        private readonly IUserTokenAppService _userTokenAppService;

        public ApiUserController(IUserAppService userAppService, IUserTokenAppService userTokenAppService)
        {
            this._userAppService = userAppService;
            this._userTokenAppService = userTokenAppService;
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
        [Route("Register")]
        [HttpPost]
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

        [Route("Login")]
        [HttpPost]
        public ApiResponse Login([FromBody]LoginViewModel login)
        {
            UserViewModel model = Mapper.Map<UserViewModel>(this._userAppService.GetByLogin(login.Email, login.Password));
            
            if(model != null)
            {
                UserTokenViewModel userToken = new UserTokenViewModel();
                userToken.Inserted = DateTime.Now;
                userToken.UserId = model.Id;
                userToken.Token = "";

                string token = this._userTokenAppService.Add(Mapper.Map<UserToken>(userToken));
                if(!string.IsNullOrEmpty(token))
                {
                    model.Token = token;
                    return new ApiResponse(true, model);
                }
                else
                    return new ApiResponse(false, "Erro ao gravar o token.");
            }
            else
                return new ApiResponse(false, "E-mail ou senha inválido.");
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
