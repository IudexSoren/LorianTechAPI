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
    [Route("api/tiposcomponente")]
    [ApiController]
    public class TiposComponenteController : ControllerBase
    {
        private readonly TipoComponenteService _tipoComponenteService;
        private readonly IMapper _mapper;

        public TiposComponenteController(TipoComponenteService tipoComponenteService, IMapper mapper)
        {
            this._tipoComponenteService = tipoComponenteService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoComponente(int id)
        {
            var result = await _tipoComponenteService.ReadTipoComponente(id);
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
        public async Task<IActionResult> GetAllTipoComponente()
        {
            var result = await _tipoComponenteService.ReadAllTipoComponente();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoComponente(DTOTipoComponenteCreate tipoComponenteCreateDTO)
        {
            TipoComponenteC_DTOValidator validator = new TipoComponenteC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoComponenteCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoComponente = _mapper.Map<TipoComponente>(tipoComponenteCreateDTO);
            var result = await _tipoComponenteService.CreateTipoComponente(tipoComponente);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoComponente(int id, DTOTipoComponenteUpdate tipoComponenteUpdateDTO)
        {
            TipoComponenteU_DTOValidator validator = new TipoComponenteU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoComponenteUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoComponente = _mapper.Map<TipoComponente>(tipoComponenteUpdateDTO);
            var result = await _tipoComponenteService.UpdateTipoComponente(id, tipoComponente);
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
        public async Task<IActionResult> DeleteTipoComponente(int id)
        {
            var result = await _tipoComponenteService.DeleteTipoComponente(id);
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
