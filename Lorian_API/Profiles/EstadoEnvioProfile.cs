using AutoMapper;
using ENTITIES.Entities;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class EstadoEnvioProfile : Profile
    {
        public EstadoEnvioProfile()
        {
            CreateMap<DTOEstadoComponenteCreate, EstadoEnvio>();
            CreateMap<DTOEstadoComponenteUpdate, EstadoEnvio>();
        }
    }
}
