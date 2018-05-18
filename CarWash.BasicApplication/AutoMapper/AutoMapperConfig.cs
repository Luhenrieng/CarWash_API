using AutoMapper;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
using BasicDDD.Domain.Entities.ValueObjects;
using System.Collections.Generic;

namespace BasicDDD.BasicApplication.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<UserTokenViewModel, UserToken>();
            CreateMap<UserToken, UserTokenViewModel>();
            CreateMap<ServiceDescription, ServiceDescriptionViewModel>();
            CreateMap<ServiceDescriptionViewModel, ServiceDescription>();
            CreateMap<ServiceViewModel, Service>();
            CreateMap<Service, ServiceViewModel>();
        }
    }
}