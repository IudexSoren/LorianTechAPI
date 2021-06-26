using AutoMapper;
using ENTITIES.Entities;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class CaracteristicaProfile : Profile
    {
        public CaracteristicaProfile()
        {
            CreateMap<DTOCaracteristicaCreate, Caracteristica>();
            CreateMap<DTOCaracteristicaUpdate, Caracteristica>();
        }
    }
}
