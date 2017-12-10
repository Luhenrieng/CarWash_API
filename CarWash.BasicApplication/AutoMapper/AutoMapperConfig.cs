using AutoMapper;
using BasicDDD.BasicApplication.Models;
using BasicDDD.Domain.Entities;
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
        }
    }
}