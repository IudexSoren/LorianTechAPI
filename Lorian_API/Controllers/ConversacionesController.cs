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
    public class ConversacionesController : ControllerBase
    {
        private readonly ConversacionService _conversacionService;
        private readonly IMapper _mapper;

        public ConversacionesController(ConversacionService conversacionService, IMapper mapper)
        {
            this._conversacionService = conversacionService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversacion(int id)
        {
            var result = await _conversacionService.ReadConversacion(id);
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
        public async Task<IActionResult> GetAllConversacion()
        {
            var result = await _conversacionService.ReadAllConversacion();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversacion(DTOConversacionCreate conversacionCreateDTO)
        {
            ConversacionC_DTOValidator validator = new ConversacionC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(conversacionCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var conversacion = _mapper.Map<Conversacion>(conversacionCreateDTO);
            var result = await _conversacionService.CreateConversacion(conversacion);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConversacion(int id, DTOConversacionUpdate conversacionUpdateDTO)
        {
            ConversacionU_DTOValidator validator = new ConversacionU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(conversacionUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var conversacion = _mapper.Map<Conversacion>(conversacionUpdateDTO);
            var result = await _conversacionService.UpdateConversacion(id, conversacion);
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
        public async Task<IActionResult> DeleteConversacion(int id)
        {
            var result = await _conversacionService.DeleteConversacion(id);
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
