using AutoMapper;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.Services;
using Lorian_API.DTOs;
using Lorian_API.Helpers;
using Lorian_API.Validators;
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
    public class EstadosUsuarioController : ControllerBase
    {
        private readonly EstadoUsuarioService _estadoUsuarioService;
        private readonly IMapper _mapper;

        public EstadosUsuarioController(EstadoUsuarioService estadoUsuarioService, IMapper mapper)
        {
            this._estadoUsuarioService = estadoUsuarioService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoUsuario(int id)
        {
            var result = await _estadoUsuarioService.ReadEstadoUsuario(id);
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
        public async Task<IActionResult> GetAllEstadoUsuario()
        {
            var result = await _estadoUsuarioService.ReadAllEstadoUsuario();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstadoUsuario(DTOEstadoUsuarioCreate estadoUsuarioCreateDTO)
        {
            EstadoUsuarioC_DTOValidator validator = new EstadoUsuarioC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoUsuarioCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoUsuario>(estadoUsuarioCreateDTO);
            var result = await _estadoUsuarioService.CreateEstadoUsuario(estado);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstadoUsuario(int id, DTOEstadoUsuarioUpdate estadoUsuarioUpdateDTO)
        {
            EstadoUsuarioU_DTOValidator validator = new EstadoUsuarioU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoUsuarioUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoUsuario>(estadoUsuarioUpdateDTO);
            var result = await _estadoUsuarioService.UpdateEstadoUsuario(id, estado);
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
        public async Task<IActionResult> DeleteEstadoUsuario(int id)
        {
            var result = await _estadoUsuarioService.DeleteEstadoUsuario(id);
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
