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
    public class CaracteristicasController : ControllerBase
    {
        private readonly CaracteristicaService _caracteristicaService;
        private readonly IMapper _mapper;

        public CaracteristicasController(CaracteristicaService caracteristicaService, IMapper mapper)
        {
            this._caracteristicaService = caracteristicaService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaracteristica(int id)
        {
            var result = await _caracteristicaService.ReadCaracteristica(id);
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
        public async Task<IActionResult> GetAllCaracteristica()
        {
            var result = await _caracteristicaService.ReadAllCaracteristica();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCaracteristica(DTOCaracteristicaCreate caracteristicaCreateDTO)
        {
            CaracteristicaC_DTOValidator validator = new CaracteristicaC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(caracteristicaCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var caracteristica = _mapper.Map<Caracteristica>(caracteristicaCreateDTO);
            var result = await _caracteristicaService.CreateCaracteristica(caracteristica);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCaracteristica(int id, DTOCaracteristicaUpdate caracteristicaUpdateDTO)
        {
            CaracteristicaU_DTOValidator validator = new CaracteristicaU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(caracteristicaUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var caracteristica = _mapper.Map<Caracteristica>(caracteristicaUpdateDTO);
            var result = await _caracteristicaService.UpdateCaracteristica(id, caracteristica);
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
        public async Task<IActionResult> DeleteCaracteristica(int id)
        {
            var result = await _caracteristicaService.DeleteCaracteristica(id);
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
