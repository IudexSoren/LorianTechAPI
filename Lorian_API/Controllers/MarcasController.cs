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
    [Route("api/marcas")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly MarcaService _marcaService;
        private readonly IMapper _mapper;

        public MarcasController(MarcaService marcaService, IMapper mapper)
        {
            this._marcaService = marcaService;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarca(int id)
        {
            var result = await _marcaService.ReadMarca(id);
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
        public async Task<IActionResult> GetAllMarca()
        {
            var result = await _marcaService.ReadAllMarca();
            switch (result.Success)
            {
                case true:
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateMarca([FromForm] DTOMarcaCreate marcaCreateDTO, [FromForm] FileUpload file)
        {
            MarcaC_DTOValidator validator = new MarcaC_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(marcaCreateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var marca = _mapper.Map<Marca>(marcaCreateDTO);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                marca.RutaImagen = Archivo.GenerarRutaArchivo(marca.Nombre, "Marca", tipoArchivo);
            }
            else
            {
                marca.RutaImagen = "";
            }

            var result = await _marcaService.CreateMarca(marca);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        await Archivo.Guardar(marca.RutaImagen, file.file);
                    }
                    return Ok(result);

                case false:
                    return StatusCode(500, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarca(int id, [FromForm] DTOMarcaUpdate marcaUpdateDTO, [FromForm] FileUpload file)
        {
            MarcaU_DTOValidator validator = new MarcaU_DTOValidator();
            ValidationResult results = await validator.ValidateAsync(marcaUpdateDTO);
            if (!results.IsValid)
            {
                return BadRequest(Validator.ListarErrores(results.Errors));
            }

            var marca = _mapper.Map<Marca>(marcaUpdateDTO);
            var marcaExists = await ElementExistsService.MarcaExists(id);
            if (file.file != null)
            {
                string tipoArchivo = Archivo.ObtenerTipoArchivo(file.file.FileName);
                marca.RutaImagen = Archivo.GenerarRutaArchivo(marca.Nombre, "Marca", tipoArchivo);
            } else
            {
                marca.RutaImagen = (marcaExists != null) ? marcaExists.RutaImagen : "";
            }

            var result = await _marcaService.UpdateMarca(id, marca);
            switch (result.Success)
            {
                case true:

                    if (result.ResultSet)
                    {
                        if (marcaExists != null)
                        {
                            Archivo.Eliminar(marcaExists.RutaImagen);
                            await Archivo.Guardar(marca.RutaImagen, file.file);
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
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var marcaExists = await ElementExistsService.MarcaExists(id);
            var result = await _marcaService.DeleteMarca(id);
            switch (result.Success)
            {
                case true:
                    if (result.ResultSet)
                    {
                        if (marcaExists != null)
                        {
                            Archivo.Eliminar(marcaExists.RutaImagen);
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
