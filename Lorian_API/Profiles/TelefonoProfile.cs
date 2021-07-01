using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class TelefonoProfile : Profile
    {
        public TelefonoProfile()
        {
            CreateMap<DTOTelefonoCreate, Telefono>();
            CreateMap<DTOTelefonoUpdate, Telefono>();
        }
    }
}
