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
    public class EstadosComponenteController : ControllerBase
    {
        private readonly EstadoComponenteService _estadoComponenteService;
        private readonly IMapper _mapper;

        public EstadosComponenteController(EstadoComponenteService estadoComponenteService, IMapper mapper)
        {
            this._estadoComponenteService = estadoComponenteService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoComponente(int id)
        {
            var result = await _estadoComponenteService.ReadEstadoComponente(id);
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
        public async Task<IActionResult> GetAllEstadoComponente()
        {
            var result = await _estadoComponenteService.ReadAllEstadoComponente();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstadoComponente(DTOEstadoComponenteCreate estadoComponenteCreateDTO)
        {
            EstadoComponenteC_DTOValidator validator = new EstadoComponenteC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoComponenteCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoComponente>(estadoComponenteCreateDTO);
            var result = await _estadoComponenteService.CreateEstadoComponente(estado);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstadoComponente(int id, DTOEstadoComponenteUpdate estadoComponenteUpdateDTO)
        {
            EstadoComponenteU_DTOValidator validator = new EstadoComponenteU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(estadoComponenteUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var estado = _mapper.Map<EstadoComponente>(estadoComponenteUpdateDTO);
            var result = await _estadoComponenteService.UpdateEstadoComponente(id, estado);
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
        public async Task<IActionResult> DeleteEstadoComponente(int id)
        {
            var result = await _estadoComponenteService.DeleteEstadoComponente(id);
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
