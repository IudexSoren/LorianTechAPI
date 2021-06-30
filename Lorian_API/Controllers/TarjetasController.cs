using AutoMapper;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using FluentValidation.Results;
using LOGIC.Services;
using LOGIC.Validators;
using LOGIC.Validators.Tarjeta;
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
    public class TarjetasController : ControllerBase
    {
        private readonly TarjetaService _tarjetaService;
        private readonly IMapper _mapper;

        public TarjetasController(TarjetaService rolService, IMapper mapper)
        {
            this._tarjetaService = rolService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarjeta(int id)
        {
            var result = await _tarjetaService.ReadTarjeta(id);
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
        public async Task<IActionResult> GetAllTarjeta(int? idUsuario)
        {
            var result = await _tarjetaService.ReadAllTarjeta(idUsuario);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarjeta(DTOTarjetaCreate tarjetaCreateDTO)
        {
            TarjetaC_DTOValidator validator = new TarjetaC_DTOValidator();
            Console.WriteLine(Regex.IsMatch(tarjetaCreateDTO.Numero, "([0-9]{4}-*){19}"));
            ValidationResult results = await validator.ValidateAsync(tarjetaCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tarjeta = _mapper.Map<Tarjeta>(tarjetaCreateDTO);
            var result = await _tarjetaService.CreateTarjeta(tarjeta);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarjeta(int id, DTOTarjetaUpdate tarjetaUpdateDTO)
        {
            TarjetaU_DTOValidator validator = new TarjetaU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tarjetaUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tarjeta = _mapper.Map<Tarjeta>(tarjetaUpdateDTO);
            var result = await _tarjetaService.UpdateTarjeta(id, tarjeta);
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
        public async Task<IActionResult> DeleteTarjeta(int id)
        {
            var result = await _tarjetaService.DeleteTarjeta(id);
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
