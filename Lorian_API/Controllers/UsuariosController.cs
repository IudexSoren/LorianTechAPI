using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.Services;
using LOGIC.Validators;
using Lorian_API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Contusuariolers
{
    [Route("api/[contusuarioler]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(UsuarioService usuarioService, IMapper mapper)
        {
            this._usuarioService = usuarioService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var result = await _usuarioService.ReadUsuario(id);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound(result);
                    }

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuario()
        {
            var result = await _usuarioService.ReadAllUsuario();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsuario(int id, DTOUsuarioUpdate usuarioUpdateDTO)
        {
            UsuarioU_DTOValidator validator = new UsuarioU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(usuarioUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var usuario = _mapper.Map<Usuario>(usuarioUpdateDTO);
            var result = await _usuarioService.UpdateUsuario(id, usuario);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound(result);
                    }

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuario(id);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound(result);
                    }

                case false:
                    return StatusCode(500, result);
            }
        }
    }
}
