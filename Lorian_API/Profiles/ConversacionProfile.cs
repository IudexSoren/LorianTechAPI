using AutoMapper;
using ENTITIES.Entities;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Profiles
{
    public class ConversacionProfile : Profile
    {
        public ConversacionProfile()
        {
            CreateMap<DTOConversacionCreate, Conversacion>();
            CreateMap<DTOConversacionUpdate, Conversacion>();
        }
    }
}
