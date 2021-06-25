using AutoMapper;
using ENTITIES.Entities;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class TipoUsuarioMensajeProfile : Profile
    {
        public TipoUsuarioMensajeProfile()
        {
            CreateMap<DTOTipoUsuarioMensajeCreate, TipoUsuarioMensaje>();
            CreateMap<DTOTipoUsuarioMensajeUpdate, TipoUsuarioMensaje>();
        }
    }
}
