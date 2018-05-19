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
        private readonly IServiceAppService _serviceAppService;

        public ApiUserController(IUserAppService userAppService, IUserTokenAppService userTokenAppService, IServiceAppService serviceAppService)
        {
            this._userAppService = userAppService;
            this._userTokenAppService = userTokenAppService;
            this._serviceAppService = serviceAppService;
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

        //[Route("ListServices")]
        //[HttpPost]
        //public ApiResponse ListServices([FromBody]int userId)
        //{
        //    List<ServiceDescriptionViewModel> services = Mapper.Map<List<ServiceDescriptionViewModel>>(this._userAppService.ListService(userId));

        //    return new ApiResponse(true, services);
        //}

        [Route("AddServiceToWasher")]
        [HttpPost]
        public ApiResponse AddServiceToWasher([FromBody]Models.ApiRequest.AddServiceToWasherRequest request)
        {
            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            try
            {
                ServicesXUser serviceUser = new ServicesXUser();
                serviceUser.ServiceId = request.ServiceId;
                serviceUser.UserId = userViewModel.Id;
                serviceUser.SpecificPrice = request.SpecificPrice;
                this._serviceAppService.AddServiceToWasher(serviceUser);

                return new ApiResponse(true, "Serviço adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }

        [Route("ListAllServices")]
        [HttpPost]
        public ApiResponse ListAllServices([FromBody]Models.ApiRequest.UserTokenRequest request)
        {
            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
            {
                return new ApiResponse(false, "Token inválido.");
            }

            List<ServiceViewModel> services = Mapper.Map<List<ServiceViewModel>>(this._serviceAppService.ListAllServices());

            return new ApiResponse(true, services);
        }

        [Route("ListServicesByWasher")]
        [HttpPost]
        public ApiResponse ListServicesByWasher([FromBody]Models.ApiRequest.ListServicesRequest request)
        {
            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
            {
                return new ApiResponse(false, "Token inválido.");
            }

            List<ServiceDescriptionViewModel> services = Mapper.Map<List<ServiceDescriptionViewModel>>(this._userAppService.ListService(request.WasherId));

            return new ApiResponse(true, services);
        }

        [Route("ListWashers")]
        [HttpPost]
        public ApiResponse ListWashers([FromBody]Models.ApiRequest.ListWashersRequest request)
        {
            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if(userViewModel == null)
            {
                return new ApiResponse(false, "Token inválido.");
            }

            List<UserViewModel> listUser = Mapper.Map<List<UserViewModel>>(this._userAppService.List());
            List<UserViewModel> listUserResponse = new List<UserViewModel>();

            if (listUser != null)
            {
                decimal requestLat = Convert.ToDecimal(request.Latitude.Replace(".",",").Substring(0, request.Latitude.IndexOf(',') + 4).Replace(",",""));
                decimal requestLng = Convert.ToDecimal(request.Longitude.Replace(".", ",").Substring(0, request.Longitude.IndexOf(',') + 4).Replace(",", ""));
                decimal maxLat, minLat, maxLng, minLng;
                decimal radius = request.MaxRadius * 3;
                
                maxLat = requestLat + radius;
                minLat = requestLat - radius;
                maxLng = requestLng + radius;
                minLng = requestLng - radius;

                foreach (var user in listUser.Where(c => c.Active).Where(c => c.RoleId == 2 || c.RoleId == 3))
                {
                    decimal lat = Convert.ToDecimal(user.Latitude.Substring(0, user.Latitude.IndexOf(',') + 4).Replace(",", ""));
                    decimal lng = Convert.ToDecimal(user.Longitude.Substring(0, user.Longitude.IndexOf(',') + 4).Replace(",", ""));

                    if((lat < maxLat && lat > minLat) && 
                        (lng < maxLng && lng > minLng))
                    {
                        listUserResponse.Add(user);
                    }
                }

                return new ApiResponse(true, listUserResponse);
            }
            return new ApiResponse(true, "");
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
