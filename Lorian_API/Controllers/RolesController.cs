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
    public class RolesController : ControllerBase
    {
        private readonly RolService _rolService;
        private readonly IMapper _mapper;

        public RolesController(RolService rolService, IMapper mapper)
        {
            this._rolService = rolService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRol(int id)
        {
            var result = await _rolService.ReadRol(id);
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
        public async Task<IActionResult> GetAllRol()
        {
            var result = await _rolService.ReadAllRol();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRol(DTORolCreate rolCreateDTO)
        {
            RolC_DTOValidator validator = new RolC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(rolCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var rol = _mapper.Map<Rol>(rolCreateDTO);
            var result = await _rolService.CreateRol(rol);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, DTORolUpdate rolUpdateDTO)
        {
            RolU_DTOValidator validator = new RolU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(rolUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var rol = _mapper.Map<Rol>(rolUpdateDTO);
            var result = await _rolService.UpdateRol(id, rol);
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
        public async Task<IActionResult> DeleteRol(int id)
        {
            var result = await _rolService.DeleteRol(id);
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
