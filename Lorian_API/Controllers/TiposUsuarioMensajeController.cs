using AutoMapper;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.Services;
using ENTITIES.DTOs;
using Lorian_API.Helpers;
using LOGIC.Validators;
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
    public class TiposUsuarioMensajeController : ControllerBase
    {
        private readonly TipoUsuarioMensajeService _tipoUsuarioMensajeService;
        private readonly IMapper _mapper;

        public TiposUsuarioMensajeController(TipoUsuarioMensajeService tipoUsuarioMensajeService, IMapper mapper)
        {
            this._tipoUsuarioMensajeService = tipoUsuarioMensajeService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoUsuarioMensaje(int id)
        {
            var result = await _tipoUsuarioMensajeService.ReadTipoUsuarioMensaje(id);
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
        public async Task<IActionResult> GetAllTipoUsuarioMensaje()
        {
            var result = await _tipoUsuarioMensajeService.ReadAllTipoUsuarioMensaje();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoUsuarioMensaje(DTOTipoUsuarioMensajeCreate tipoUsuarioMensajeCreateDTO)
        {
            TipoUsuarioMensajeC_DTOValidator validator = new TipoUsuarioMensajeC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoUsuarioMensajeCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoUsuarioMensaje = _mapper.Map<TipoUsuarioMensaje>(tipoUsuarioMensajeCreateDTO);
            var result = await _tipoUsuarioMensajeService.CreateTipoUsuarioMensaje(tipoUsuarioMensaje);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoUsuarioMensaje(int id, DTOTipoUsuarioMensajeUpdate tipoUsuarioMensajeUpdateDTO)
        {
            TipoUsuarioMensajeU_DTOValidator validator = new TipoUsuarioMensajeU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoUsuarioMensajeUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoUsuarioMensaje = _mapper.Map<TipoUsuarioMensaje>(tipoUsuarioMensajeUpdateDTO);
            var result = await _tipoUsuarioMensajeService.UpdateTipoUsuarioMensaje(id, tipoUsuarioMensaje);
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
        public async Task<IActionResult> DeleteTipoUsuarioMensaje(int id)
        {
            var result = await _tipoUsuarioMensajeService.DeleteTipoUsuarioMensaje(id);
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
