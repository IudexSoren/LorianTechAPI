using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class LineaDetalleProfile : Profile
    {
        public LineaDetalleProfile()
        {
            CreateMap<DTOLineaDetalleCreate, LineaDetalle>();
            CreateMap<LineaDetalle, DTOLineaDetalleRead>();
        }
    }
}
