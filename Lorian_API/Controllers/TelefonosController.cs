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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lorian_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly TelefonoService _telefonoService;
        private readonly IMapper _mapper;

        public TelefonosController(TelefonoService telefonoService, IMapper mapper)
        {
            this._telefonoService = telefonoService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTelefono(int id)
        {
            var result = await _telefonoService.ReadTelefono(id);
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
        public async Task<IActionResult> GetAllTelefono(int? idUsuario)
        {
            var result = await _telefonoService.ReadAllTelefono(idUsuario);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTelefono(DTOTelefonoCreate telefonoCreateDTO)
        {
            TelefonoC_DTOValidator validator = new TelefonoC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(telefonoCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var telefono = _mapper.Map<Telefono>(telefonoCreateDTO);
            var result = await _telefonoService.CreateTelefono(telefono);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTelefono(int id, DTOTelefonoUpdate telefonoUpdateDTO)
        {
            TelefonoU_DTOValidator validator = new TelefonoU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(telefonoUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var telefono = _mapper.Map<Telefono>(telefonoUpdateDTO);
            var result = await _telefonoService.UpdateTelefono(id, telefono);
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
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var result = await _telefonoService.DeleteTelefono(id);
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
