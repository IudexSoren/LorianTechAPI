using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.LogicEntities;
using LOGIC.Services;
using LOGIC.Validators;
using Lorian_API.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DTOUsuarioCreate usuarioCreateDTO)
        {
            UsuarioC_DTOValidator validator = new UsuarioC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(usuarioCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var usuario = _mapper.Map<Usuario>(usuarioCreateDTO);
            var result = await _usuarioService.CreateUsuario(usuario);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(DTOUsuarioLogin usuarioLoginDTO)
        {
            UsuarioLogin_DTOValidator validator = new UsuarioLogin_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(usuarioLoginDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            try
            {
                UsuarioLogic usuarioLogic = new UsuarioLogic();
                DTOUsuarioRead dtoUsuario = await usuarioLogic.Validar(this._mapper, usuarioLoginDTO.Email, usuarioLoginDTO.Clave);

                ClaimsPrincipal claimsPrincipal = usuarioLogic.CreateClaimsPrincipal(dtoUsuario);
                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok(dtoUsuario);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Credenciales"))
                {
                    return BadRequest(exception.Message);
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Unauthorized("Usted no tiene permisos para acceder a este contenido");
        }
    }
}
