using AutoMapper;
using ENTITIES.Entities;
using FluentValidation.Results;
using ENTITIES.DTOs;
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
    public class ComponentesController : ControllerBase
    {
        private readonly ComponenteService _componenteService;
        private readonly IMapper _mapper;

        public ComponentesController(ComponenteService componenteService, IMapper mapper)
        {
            this._componenteService = componenteService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponente(int id)
        {
            var result = await _componenteService.ReadComponente(id);
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
        public async Task<IActionResult> GetAllComponente(int? idTipoComponente, int? idMarca, int? idEstadoComponente)
        {
            var result = await _componenteService.ReadAllComponente(idTipoComponente, idMarca, idEstadoComponente);
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateComponente([FromForm] DTOComponenteCreate componenteCreateDTO, [FromForm] FileUpload file)
        {
            ComponenteC_DTOValidator validator = new ComponenteC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(componenteCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var componente = _mapper.Map<Componente>(componenteCreateDTO);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                componente.RutaImagen = Archivo.GenerarRutaArchivo(componente.Nombre, "Componente", tipoArchivo);
            }
            else
            {
                componente.RutaImagen = "";
            }

            var result = await _componenteService.CreateComponente(componente, componenteCreateDTO.IdsCaracteristicas, componenteCreateDTO.IdsPromociones);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        await Archivo.Guardar(componente.RutaImagen, file.file);
                    }
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComponente(int id, [FromForm] DTOComponenteUpdate componenteUpdateDTO, [FromForm] FileUpload file)
        {
            ComponenteU_DTOValidator validator = new ComponenteU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(componenteUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var componente = _mapper.Map<Componente>(componenteUpdateDTO);
            var componenteExists = await ComponenteService.ReadSimple(id);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                componente.RutaImagen = Archivo.GenerarRutaArchivo(componente.Nombre, "Componente", tipoArchivo);
            }
            else
            {
                componente.RutaImagen = (componenteExists != null) ? componenteExists.RutaImagen : "";
            }

            var result = await _componenteService.UpdateComponente(id, componente, componenteUpdateDTO.IdsCaracteristicas, componenteUpdateDTO.IdsPromociones);
            switch (result.Success)
            {
                case true:

                    if (result.ResultSet)
                    {
                        if (componenteExists != null)
                        {
                            Archivo.Eliminar(componenteExists.RutaImagen);
                            await Archivo.Guardar(componente.RutaImagen, file.file);
                        }
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
        public async Task<IActionResult> DeleteComponente(int id)
        {
            var componenteExists = await ComponenteService.ReadSimple(id);
            var result = await _componenteService.DeleteComponente(id);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        if (componenteExists != null)
                        {
                            Archivo.Eliminar(componenteExists.RutaImagen);
                        }
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
