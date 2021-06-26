using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class ComponenteProfile : Profile
    {
        public ComponenteProfile()
        {
            CreateMap<DTOComponenteCreate, Componente>();
            CreateMap<DTOComponenteUpdate, Componente>();
            CreateMap<Componente, DTOComponenteRead>();
        }
    }
}
