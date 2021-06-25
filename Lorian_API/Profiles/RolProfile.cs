using AutoMapper;
using ENTITIES.Entities;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<DTORolCreate, Rol>();
            CreateMap<DTORolUpdate, Rol>();
        }
    }
}
