using AutoMapper;
using ENTITIES.Entities;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class PromocionProfile : Profile
    {
        public PromocionProfile()
        {
            CreateMap<DTOPromocionCreate, Promocion>();
            CreateMap<DTOPromocionUpdate, Promocion>();
        }
    }
}
