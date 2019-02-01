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
using System.Web.Http.Cors;
using BasicDDD.Domain.Entities.ValueObjects;
using CarWash.Util.Moip;

namespace BasicDDD.BasicApplication.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/ApiUser")]
    [EnableCors(origins: "http://localhost, http://localhost:80, http://localhost:4200, http://www.carwash.ml, http://carwash.ml, https://sandbox.moip.com.br", headers: "*", methods: "*")]
    public class ApiUserController : ApiController
    {

        private readonly IUserAppService _userAppService;
        private readonly IUserTokenAppService _userTokenAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderedAppService _orderedAppService;
        private readonly IEvaluationAppService _evaluationAppService;

        public ApiUserController(IUserAppService userAppService, IUserTokenAppService userTokenAppService, IServiceAppService serviceAppService, IOrderedAppService orderedAppService, IEvaluationAppService evaluationAppService)
        {
            this._userAppService = userAppService;
            this._userTokenAppService = userTokenAppService;
            this._serviceAppService = serviceAppService;
            this._orderedAppService = orderedAppService;
            this._evaluationAppService = evaluationAppService;
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


        [Route("AddServiceToWasher")]
        [HttpPost]
        public ApiResponse AddServiceToWasher([FromBody]Models.ApiRequest.AddServiceToWasherRequest request)
        {
            if(request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

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
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

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
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

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
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if(userViewModel == null)
            {
                return new ApiResponse(false, "Token inválido.");
            }

            try
            {
                List<WasherViewModel> listUser = Mapper.Map<List<WasherViewModel>>(this._userAppService.ListWasher());
                List<WasherViewModel> listUserResponse = new List<WasherViewModel>();

                if (listUser != null)
                {
                    decimal requestLat = 0;
                    decimal requestLng = 0;

                    try
                    {
                        var indexCharLat = request.Latitude.Contains(".") ? request.Latitude.IndexOf('.') : request.Latitude.IndexOf(',');
                        var indexCharLong = request.Longitude.Contains(".") ? request.Longitude.IndexOf('.') : request.Longitude.IndexOf(',');

                        requestLat = Convert.ToDecimal(request.Latitude.Replace(".", ",").Substring(0, indexCharLat + 3).Replace(",", ""));
                        requestLng = Convert.ToDecimal(request.Longitude.Replace(".", ",").Substring(0, indexCharLong + 3).Replace(",", ""));
                    }
                    catch (Exception ex)
                    {
                        return new ApiResponse(false, "Campos 'latitude' ou 'longitude' estão no formato incorreto.");
                    }
                    
                    decimal maxLat, minLat, maxLng, minLng;
                    decimal radius = request.MaxRadius;

                    maxLat = requestLat + radius;
                    minLat = requestLat - radius;
                    maxLng = requestLng + radius;
                    minLng = requestLng - radius;

                    foreach (var user in listUser.Where(c => c.Active).Where(c => c.RoleId == 2 || c.RoleId == 3))
                    {
                        var indexCharLat = request.Latitude.Contains(".") ? request.Latitude.IndexOf('.') : request.Latitude.IndexOf(',');
                        var indexCharLong = request.Longitude.Contains(".") ? request.Longitude.IndexOf('.') : request.Longitude.IndexOf(',');

                        decimal lat = Convert.ToDecimal(user.Latitude.Replace(".",",").Substring(0, indexCharLat + 3).Replace(",", ""));
                        decimal lng = Convert.ToDecimal(user.Longitude.Replace(".", ",").Substring(0, indexCharLong + 3).Replace(",", ""));

                        if ((lat < maxLat && lat > minLat) &&
                            (lng < maxLng && lng > minLng))
                        {
                            listUserResponse.Add(user);
                        }
                    }

                    return new ApiResponse(true, listUserResponse);
                }
                return new ApiResponse(true, "");
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
            
        }


        // PUT: api/ApiUser/5
        public void Put(int id, [FromBody]string value)
        {
            string str = value;
        }


        // DELETE: api/ApiUser/5
        public void Delete(int id)
        {
        }


        [Route("Login")]
        [HttpPost]
        public ApiResponse Login([FromBody]LoginViewModel login)
        {
            if (login == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");
            else if(String.IsNullOrEmpty(login.Email))
                return new ApiResponse(false, "E-mail não pode ser nulo.");
            else if (String.IsNullOrEmpty(login.Password))
                return new ApiResponse(false, "Senha não pode ser nula.");

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


        [Route("CreateOrder")]
        [HttpPost]
        public ApiResponse CreateOrder([FromBody]Models.CreateOrderViewModel order)
        {
            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(order.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            try
            {
                string validationMessage = _orderedAppService.ValidadeOrder(Mapper.Map<Domain.Entities.ValueObjects.CreateOrder>(order));

                if (validationMessage == "")
                {
                    int orderId = _orderedAppService.CreateOrder(Mapper.Map<Domain.Entities.ValueObjects.CreateOrder>(order));

                    if (orderId > 0)
                    {
                        CreateOrderResponse response = new CreateOrderResponse();
                        response.OrderId = orderId;
                        response.Message = "Pedido criado com sucesso.";
                        return new ApiResponse(true, response);
                    }
                    else
                        return new ApiResponse(false, "Erro ao criar pedido, por favor tente novamente mais tarde.");
                }
                else
                {
                    return new ApiResponse(false, validationMessage);
                }
               
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }


        [Route("ListAllOrders")]
        [HttpPost]
        public ApiResponse ListAllOrders([FromBody]Models.ApiRequest.UserTokenRequest request)
        {
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            try
            {
                List<OrderReportViewModel> listOrders = Mapper.Map<List<OrderReportViewModel>>(this._orderedAppService.ListAllOrderReport().ToList());
                return new ApiResponse(true, listOrders);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }


        [Route("ListOrderByUser")]
        [HttpPost]
        public ApiResponse ListOrderByUser([FromBody]Models.ApiRequest.UserTokenRequest request)
        {
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            try
            {
                List<OrderReportViewModel> listOrders = Mapper.Map<List<OrderReportViewModel>>(this._orderedAppService.ListOrderByUser(userViewModel.Id, userViewModel.RoleId).ToList());
                return new ApiResponse(true, listOrders);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }


        [Route("EvaluateUser")]
        [HttpPost]
        public ApiResponse EvaluateUser([FromBody]Models.ApiRequest.EvaluateUserRequest request)
        {
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");
            

            string message = this._evaluationAppService.Add(Mapper.Map<Domain.Entities.ValueObjects.EvaluateUser>(request));

            if (message == "")
                return new ApiResponse(true, "Avaliação registrada com sucesso.");
            else
                return new ApiResponse(false, message);
        }


        [Route("AcceptOrder")]
        [HttpPost]
        public ApiResponse AcceptOrder([FromBody]Models.ApiRequest.UpdateOrderStatusRequest request)
        {
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            request.WasherId = userViewModel.Id;
            request.Status = 4; //Aceito
            string message = this._orderedAppService.UpdateOrderStatus(Mapper.Map<UpdateOrderStatus>(request));

            if (message == "")
                return new ApiResponse(true, "Pedido aceito com sucesso.");
            else
                return new ApiResponse(false, message);
        }


        [Route("RejectOrder")]
        [HttpPost]
        public ApiResponse RejectOrder([FromBody]Models.ApiRequest.UpdateOrderStatusRequest request)
        {
            if (request == null)
                return new ApiResponse(false, "Objeto de entrada no formato incorreto ou não informado.");

            UserViewModel userViewModel = Mapper.Map<UserViewModel>(this._userAppService.GetByToken(request.Token));

            if (userViewModel == null)
                return new ApiResponse(false, "Token inválido.");

            request.WasherId = userViewModel.Id;
            request.Status = 3; //Rejeitado
            string message = this._orderedAppService.UpdateOrderStatus(Mapper.Map<UpdateOrderStatus>(request));

            if (message == "")
                return new ApiResponse(true, "Pedido rejeitado com sucesso.");
            else
                return new ApiResponse(false, message);
        }


        [Route("SendPayment")]
        [HttpGet]
        public ApiResponse SendPayment([FromBody]Models.ApiRequest.UpdateOrderStatusRequest request)
        {
            Moip moip = new Moip();

            moip.FindCustomer("CUS-BD9NCWG3V7EH");

            return new ApiResponse(false, "200 OK");
        }
    }
}
