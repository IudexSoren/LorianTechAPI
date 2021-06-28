using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<DTOUsuarioCreate, Usuario>();
            CreateMap<DTOUsuarioUpdate, Usuario>();
            CreateMap<Usuario, DTOUsuarioRead>();
            CreateMap<DTOUsuarioLogin, Usuario>();
        }
    }
}
