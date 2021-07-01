using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class DireccionProfile : Profile
    {
        public DireccionProfile()
        {
            CreateMap<DTODireccionCreate, Direccion>();
            CreateMap<DTODireccionUpdate, Direccion>();
        }
    }
}
