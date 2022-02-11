using AutoMapper;
using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploCharla.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserCreatedDto, User>();

            CreateMap<User, LoginResponseDto>();
        }
    }
}
