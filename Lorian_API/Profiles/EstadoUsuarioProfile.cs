﻿using AutoMapper;
using ENTITIES.Entities;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class EstadoUsuarioProfile : Profile
    {
        public EstadoUsuarioProfile()
        {
            CreateMap<DTOEstadoUsuarioCreate, EstadoUsuario>();
            CreateMap<DTOEstadoUsuarioUpdate, EstadoUsuario>();
        }
    }
}
