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
    public class PromocionesController : ControllerBase
    {
        private readonly PromocionService _promocionService;
        private readonly IMapper _mapper;

        public PromocionesController(PromocionService promocionService, IMapper mapper)
        {
            this._promocionService = promocionService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromocion(int id)
        {
            var result = await _promocionService.ReadPromocion(id);
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
        public async Task<IActionResult> GetAllPromocion()
        {
            var result = await _promocionService.ReadAllPromocion();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromocion(DTOPromocionCreate promocionCreateDTO)
        {
            PromocionC_DTOValidator validator = new PromocionC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(promocionCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var promocion = _mapper.Map<Promocion>(promocionCreateDTO);
            var result = await _promocionService.CreatePromocion(promocion);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromocion(int id, DTOPromocionUpdate promocionUpdateDTO)
        {
            PromocionU_DTOValidator validator = new PromocionU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(promocionUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var promocion = _mapper.Map<Promocion>(promocionUpdateDTO);
            var result = await _promocionService.UpdatePromocion(id, promocion);
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
        public async Task<IActionResult> DeletePromocion(int id)
        {
            var result = await _promocionService.DeletePromocion(id);
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
