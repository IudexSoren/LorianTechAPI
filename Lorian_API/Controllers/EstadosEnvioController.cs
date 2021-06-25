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
    public class EstadosEnvioController : ControllerBase
    {
        private readonly EstadoEnvioService _estadoEnvioService;
        private readonly IMapper _mapper;

        public EstadosEnvioController(EstadoEnvioService estadoEnvioService, IMapper mapper)
        {
            this._estadoEnvioService = estadoEnvioService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoEnvio(int id)
        {
            var result = await _estadoEnvioService.ReadEstadoEnvio(id);
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
        public async Task<IActionResult> GetAllEstadoEnvio()
        {
            var result = await _estadoEnvioService.ReadAllEstadoEnvio();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstadoEnvio(DTOEstadoEnvioCreate estadoEnvioCreateDTO)
        {
            EstadoEnvioC_DTOValidator validator = new EstadoEnvioC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoEnvioCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoEnvio>(estadoEnvioCreateDTO);
            var result = await _estadoEnvioService.CreateEstadoEnvio(estado);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstadoEnvio(int id, DTOEstadoEnvioUpdate estadoEnvioUpdateDTO)
        {
            EstadoEnvioU_DTOValidator validator = new EstadoEnvioU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoEnvioUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoEnvio>(estadoEnvioUpdateDTO);
            var result = await _estadoEnvioService.UpdateEstadoEnvio(id, estado);
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
        public async Task<IActionResult> DeleteEstadoEnvio(int id)
        {
            var result = await _estadoEnvioService.DeleteEstadoEnvio(id);
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
