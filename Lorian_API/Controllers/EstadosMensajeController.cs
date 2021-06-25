using AutoMapper;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.Services;
using Lorian_API.DTOs;
using Lorian_API.Helpers;
using Lorian_API.Validators.EstadoMensaje;
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
    public class EstadosMensajeController : ControllerBase
    {
        private readonly EstadoMensajeService _estadoMensajeService;
        private readonly IMapper _mapper;

        public EstadosMensajeController(EstadoMensajeService estadoMensajeService, IMapper mapper)
        {
            this._estadoMensajeService = estadoMensajeService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoMensaje(int id)
        {
            var result = await _estadoMensajeService.ReadEstadoMensaje(id);
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
        public async Task<IActionResult> GetAllEstadoMensaje()
        {
            var result = await _estadoMensajeService.ReadAllEstadoMensaje();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstadoMensaje(DTOEstadoMensajeCreate estadoMensajeCreateDTO)
        {
            EstadoMensajeC_DTOValidator validator = new EstadoMensajeC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoMensajeCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoMensaje>(estadoMensajeCreateDTO);
            var result = await _estadoMensajeService.CreateEstadoMensaje(estado);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstadoMensaje(int id, DTOEstadoMensajeUpdate estadoMensajeUpdateDTO)
        {
            EstadoMensajeU_DTOValidator validator = new EstadoMensajeU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoMensajeUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoMensaje>(estadoMensajeUpdateDTO);
            var result = await _estadoMensajeService.UpdateEstadoMensaje(id, estado);
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
        public async Task<IActionResult> DeleteEstadoMensaje(int id)
        {
            var result = await _estadoMensajeService.DeleteEstadoMensaje(id);
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
