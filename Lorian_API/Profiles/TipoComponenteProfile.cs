using AutoMapper;
using ENTITIES.Entities;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class TipoComponenteProfile : Profile
    {
        public TipoComponenteProfile()
        {
            CreateMap<DTOTipoComponenteCreate, TipoComponente>();
            CreateMap<DTOTipoComponenteUpdate, TipoComponente>();
        }
    }
}
