using AutoMapper;
using ENTITIES.DTOs;
using LOGIC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public AuthController(UsuarioService usuarioService, IMapper mapper)
        {
            this._usuarioService = usuarioService;
            this._mapper = mapper;
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(DTOUsuarioCreate usuarioCreateDTO)
        //{

        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(DTOUsuarioLogin usuarioLoginDTO)
        //{

        //}
    }
}
