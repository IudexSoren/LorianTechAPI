using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class TarjetaProfile : Profile
    {
        public TarjetaProfile()
        {
            CreateMap<DTOTarjetaCreate, Tarjeta>();
            CreateMap<DTOTarjetaUpdate, Tarjeta>();
        }
    }
}
