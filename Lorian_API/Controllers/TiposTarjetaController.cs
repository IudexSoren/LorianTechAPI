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
    public class TiposTarjetaController : ControllerBase
    {
        private readonly TipoTarjetaService _tipoTarjetaService;
        private readonly IMapper _mapper;

        public TiposTarjetaController(TipoTarjetaService tipoTarjetaService, IMapper mapper)
        {
            this._tipoTarjetaService = tipoTarjetaService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoTarjeta(int id)
        {
            var result = await _tipoTarjetaService.ReadTipoTarjeta(id);
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
        public async Task<IActionResult> GetAllTipoTarjeta()
        {
            var result = await _tipoTarjetaService.ReadAllTipoTarjeta();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateTipoTarjeta([FromForm] DTOTipoTarjetaCreate tipoTarjetaCreateDTO, [FromForm] FileUpload file)
        {
            TipoTarjetaC_DTOValidator validator = new TipoTarjetaC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoTarjetaCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoTarjeta = _mapper.Map<TipoTarjeta>(tipoTarjetaCreateDTO);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                tipoTarjeta.RutaImagen = Archivo.GenerarRutaArchivo(tipoTarjeta.Nombre, "TipoTarjeta", tipoArchivo);
            }
            else
            {
                tipoTarjeta.RutaImagen = "";
            }

            var result = await _tipoTarjetaService.CreateTipoTarjeta(tipoTarjeta);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        await Archivo.Guardar(tipoTarjeta.RutaImagen, file.file);
                    }
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoTarjeta(int id, [FromForm] DTOTipoTarjetaUpdate tipoTarjetaUpdateDTO, [FromForm] FileUpload file)
        {
            TipoTarjetaU_DTOValidator validator = new TipoTarjetaU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(tipoTarjetaUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var tipoTarjeta = _mapper.Map<TipoTarjeta>(tipoTarjetaUpdateDTO);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                tipoTarjeta.RutaImagen = Archivo.GenerarRutaArchivo(tipoTarjeta.Nombre, "TipoTarjeta", tipoArchivo);
            }
            else
            {
                tipoTarjeta.RutaImagen = "";
            }

            var tipoTarjetaResult = await _tipoTarjetaService.ReadTipoTarjeta(id);
            var result = await _tipoTarjetaService.UpdateTipoTarjeta(id, tipoTarjeta);
            switch (result.Success)
            {
                case true:

                    if (result.ResultSet)
                    {
                        if (tipoTarjetaResult.ResultSet != null)
                        {
                            Archivo.Eliminar(tipoTarjetaResult.ResultSet.RutaImagen);
                            await Archivo.Guardar(tipoTarjeta.RutaImagen, file.file);
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
        public async Task<IActionResult> DeleteTipoTarjeta(int id)
        {
            var tipoTarjetaResult = await _tipoTarjetaService.ReadTipoTarjeta(id);
            var result = await _tipoTarjetaService.DeleteTipoTarjeta(id);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        if (tipoTarjetaResult.ResultSet != null)
                        {
                            Archivo.Eliminar(tipoTarjetaResult.ResultSet.RutaImagen);
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
