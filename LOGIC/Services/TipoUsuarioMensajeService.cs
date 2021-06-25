using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class TipoUsuarioMensajeService
    {
        private readonly TipoUsuarioMensajeRepo _tipoUsuarioMensajeRepo = new TipoUsuarioMensajeRepo();

        public async Task<Generic_ResultSet<bool>> CreateTipoUsuarioMensaje(TipoUsuarioMensaje tipoUsuarioMensaje)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tipoUsuarioMensajeRepo.Create(tipoUsuarioMensaje);

                result.UserMessage = state ?
                    $"El tipo de usuario del mensaje { tipoUsuarioMensaje.Nombre } fue creado corretamente" : $"El tipo de usuario del mensaje no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.TipoUsuarioMensajeService: CreateTipoUsuarioMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de usuario del mensaje { tipoUsuarioMensaje.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoUsuarioMensajeService: CreateTipoUsuarioMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<TipoUsuarioMensaje>> ReadTipoUsuarioMensaje(int id)
        {
            Generic_ResultSet<TipoUsuarioMensaje> result = new Generic_ResultSet<TipoUsuarioMensaje>();
            try
            {
                TipoUsuarioMensaje tipo = await _tipoUsuarioMensajeRepo.Read(id);

                result.UserMessage = tipo != null ?
                    $"El tipo de usuario del mensaje { tipo.Nombre } fue obtenido corretamente" : $"El tipo de usuario del mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoUsuarioMensajeService: ReadTipoUsuarioMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = tipo;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de usuario del mensaje no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoUsuarioMensajeService: ReadTipoUsuarioMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<TipoUsuarioMensaje>>> ReadAllTipoUsuarioMensaje()
        {
            Generic_ResultSet<List<TipoUsuarioMensaje>> result = new Generic_ResultSet<List<TipoUsuarioMensaje>>();
            try
            {
                List<TipoUsuarioMensaje> tipos = await _tipoUsuarioMensajeRepo.ReadAll();

                result.UserMessage = $"La lista de tipos de usuario del mensaje fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.TipoUsuarioMensajeService: ReadAllTipoUsuarioMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = tipos;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de tipos de usuario del mensaje";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoUsuarioMensajeService: ReadAllTipoUsuarioMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateTipoUsuarioMensaje(int id, TipoUsuarioMensaje tipoUsuarioMensaje)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tipoUsuarioMensajeRepo.Update(id, tipoUsuarioMensaje);

                result.UserMessage = state ?
                    $"El tipo de usuario del mensaje { tipoUsuarioMensaje.Nombre } fue obtenido corretamente" : $"El tipo de usuario del mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoUsuarioMensajeService: UpdateTipoUsuarioMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de usuario del mensaje { tipoUsuarioMensaje.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoUsuarioMensajeService: UpdateTipoUsuarioMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteTipoUsuarioMensaje(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tipoUsuarioMensajeRepo.Delete(id);

                result.UserMessage = state ?
                    $"El tipo de usuario del mensaje fue eliminado corretamente" : $"El tipo de usuario del mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoUsuarioMensajeService: DeleteMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de usuario del mensaje no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoUsuarioMensajeService: DeleteTipoUsuarioMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
