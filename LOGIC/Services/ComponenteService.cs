﻿using AutoMapper;
using DAL.Repositories;
using ENTITIES.DTOs;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class ComponenteService
    {
        private readonly ComponenteRepo _componenteRepo = new ComponenteRepo();
        private readonly IMapper _mapper;

        public ComponenteService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<Generic_ResultSet<bool>> CreateComponente(Componente componente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                componente.CreationDate = DateTime.UtcNow;
                componente.ModifiedDate = DateTime.UtcNow;

                bool state = await _componenteRepo.Create(componente);

                result.UserMessage = state ?
                    $"El componente { componente.Nombre } fue creado corretamente" : $"El componente no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.ComponenteService: CreateComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el componente { componente.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.ComponenteService: CreateComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<DTOComponenteRead>> ReadComponente(int id)
        {
            Generic_ResultSet<DTOComponenteRead> result = new Generic_ResultSet<DTOComponenteRead>();
            try
            {
                Componente componente = await _componenteRepo.Read(id);
                DTOComponenteRead dtoComponente = _mapper.Map<DTOComponenteRead>(componente);
                dtoComponente.TipoComponente = await ElementExistsService.TipoComponenteExists(componente.IdTipoComponente);
                dtoComponente.Marca = await ElementExistsService.MarcaExists(componente.IdMarca);
                dtoComponente.EstadoComponente = await ElementExistsService.EstadoComponenteExists(componente.IdEstadoComponente);

                result.UserMessage = componente != null ?
                    $"El componente { componente.Nombre } fue obtenido corretamente" : $"El componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.ComponenteService: ReadComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = dtoComponente;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el componente no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.ComponenteService: ReadComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<DTOComponenteRead>>> ReadAllComponente(int? idTipo, int? idMarca, int? idEstado)
        {
            Generic_ResultSet<List<DTOComponenteRead>> result = new Generic_ResultSet<List<DTOComponenteRead>>();
            try
            {
                List<Componente> componentes =
                    (idTipo.HasValue) ? await _componenteRepo.ReadAllByIdTipoComponente(idTipo.Value) :
                    (idMarca.HasValue) ? await _componenteRepo.ReadAllByIdMarca(idMarca.Value) :
                    (idEstado.HasValue) ? await _componenteRepo.ReadAllByIdEstadoComponente(idEstado.Value) :
                    await _componenteRepo.ReadAll();

                List<DTOComponenteRead> dtoListComponente = new List<DTOComponenteRead>();
                foreach (var componente in componentes)
                {
                    DTOComponenteRead dtoComponente = _mapper.Map<DTOComponenteRead>(componente);
                    dtoComponente.TipoComponente = await ElementExistsService.TipoComponenteExists(componente.IdTipoComponente);
                    dtoComponente.Marca = await ElementExistsService.MarcaExists(componente.IdMarca);
                    dtoComponente.EstadoComponente = await ElementExistsService.EstadoComponenteExists(componente.IdEstadoComponente);
                    dtoListComponente.Add(dtoComponente);
                }

                result.UserMessage = $"La lista de componentes fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.ComponenteService: ReadAllComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = dtoListComponente;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de componentes";
                result.InternalMessage = $"ERROR: LOGIC.Services.ComponenteService: ReadAllComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateComponente(int id, Componente componente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                componente.ModifiedDate = DateTime.UtcNow;

                bool state = await _componenteRepo.Update(id, componente);

                result.UserMessage = state ?
                    $"El componente { componente.Nombre } fue actualizado corretamente" : $"El componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.ComponenteService: UpdateComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el componente { componente.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.ComponenteService: UpdateComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteComponente(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _componenteRepo.Delete(id);

                result.UserMessage = state ?
                    $"El componente fue eliminado corretamente" : $"El componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.ComponenteService: DeleteComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el componente no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.ComponenteService: DeleteComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
