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

namespace Lorian_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionesController : ControllerBase
    {
        private readonly DireccionService _direccionService;
        private readonly IMapper _mapper;

        public DireccionesController(DireccionService direccionService, IMapper mapper)
        {
            this._direccionService = direccionService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDireccion(int id)
        {
            var result = await _direccionService.ReadDireccion(id);
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
        public async Task<IActionResult> GetAllDireccion(int? idUsuario)
        {
            var result = await _direccionService.ReadAllDireccion(idUsuario);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDireccion(DTODireccionCreate direccionCreateDTO)
        {
            DireccionC_DTOValidator validator = new DireccionC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(direccionCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var direccion = _mapper.Map<Direccion>(direccionCreateDTO);
            var result = await _direccionService.CreateDireccion(direccion);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDireccion(int id, DTODireccionUpdate direccionUpdateDTO)
        {
            DireccionU_DTOValidator validator = new DireccionU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(direccionUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var direccion = _mapper.Map<Direccion>(direccionUpdateDTO);
            var result = await _direccionService.UpdateDireccion(id, direccion);
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
        public async Task<IActionResult> DeleteDireccion(int id)
        {
            var result = await _direccionService.DeleteDireccion(id);
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
