using DAL.Interfaces;
using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class TipoComponenteService
    {
        private readonly TipoComponenteRepo _tipoComponenteRepo = new TipoComponenteRepo();

        public async Task<Generic_ResultSet<bool>> CreateTipoComponente(TipoComponente tipoComponente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tipoComponente.CreationDate = DateTime.UtcNow;
                tipoComponente.ModifiedDate = DateTime.UtcNow;

                bool state = await _tipoComponenteRepo.Create(tipoComponente);

                result.UserMessage = state ?
                    $"El tipo de componente { tipoComponente.Nombre } fue creado corretamente" : $"El tipo de componente no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.TipoComponenteService: CreateTipoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de componente { tipoComponente.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoComponenteService: CreateTipoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<TipoComponente>> ReadTipoComponente(int id)
        {
            Generic_ResultSet<TipoComponente> result = new Generic_ResultSet<TipoComponente>();
            try
            {
                TipoComponente tipo = await _tipoComponenteRepo.Read(id);

                result.UserMessage = tipo != null ? 
                    $"El tipo de componente { tipo.Nombre } fue obtenido corretamente" : $"El tipo de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoComponenteService: ReadTipoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = tipo;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de componente no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoComponenteService: ReadTipoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<TipoComponente>>> ReadAllTipoComponente()
        {
            Generic_ResultSet<List<TipoComponente>> result = new Generic_ResultSet<List<TipoComponente>>();
            try
            {
                List<TipoComponente> tipos = await _tipoComponenteRepo.ReadAll();

                result.UserMessage = $"La lista de tipos de componente fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.TipoComponenteService: ReadAllTipoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = tipos;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de tipos de componente";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoComponenteService: ReadAllTipoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateTipoComponente(int id, TipoComponente tipoComponente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tipoComponente.ModifiedDate = DateTime.UtcNow;

                bool state = await _tipoComponenteRepo.Update(id, tipoComponente);

                result.UserMessage = state ?
                    $"El tipo de componente { tipoComponente.Nombre } fue obtenido corretamente" : $"El tipo de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoComponenteService: UpdateTipoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de componente { tipoComponente.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoComponenteService: UpdateTipoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteTipoComponente(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tipoComponenteRepo.Delete(id);

                result.UserMessage = state ? 
                    $"El tipo de componente fue eliminado corretamente" : $"El tipo de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoComponenteService: DeleteMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de componente no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoComponenteService: DeleteTipoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<TipoComponente> ReadSimple(int id)
        {
            try
            {
                TipoComponente tipoComponente = await new TipoComponenteRepo().Read(id);

                return tipoComponente;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<TipoComponente>> ReadAllSimple()
        {
            try
            {
                List<TipoComponente> tipos = await new TipoComponenteRepo().ReadAll();

                return tipos;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
