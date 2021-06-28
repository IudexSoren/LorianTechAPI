using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class EstadoMensajeService
    {
        private readonly EstadoMensajeRepo _estadoMensajeRepo = new EstadoMensajeRepo();

        public async Task<Generic_ResultSet<bool>> CreateEstadoMensaje(EstadoMensaje estadoMensaje)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoMensajeRepo.Create(estadoMensaje);

                result.UserMessage = state ?
                    $"El estado de mensaje { estadoMensaje.Nombre } fue creado corretamente" : $"El estado de mensaje no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.EstadoMensajeService: CreateEstadoMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de mensaje { estadoMensaje.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoMensajeService: CreateEstadoMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<EstadoMensaje>> ReadEstadoMensaje(int id)
        {
            Generic_ResultSet<EstadoMensaje> result = new Generic_ResultSet<EstadoMensaje>();
            try
            {
                EstadoMensaje estado = await _estadoMensajeRepo.Read(id);

                result.UserMessage = estado != null ?
                    $"El estado de mensaje { estado.Nombre } fue obtenido corretamente" : "El estado de mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoMensajeService: ReadEstadoMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = estado;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de mensaje no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoMensajeService: ReadEstadoMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<EstadoMensaje>>> ReadAllEstadoMensaje()
        {
            Generic_ResultSet<List<EstadoMensaje>> result = new Generic_ResultSet<List<EstadoMensaje>>();
            try
            {
                List<EstadoMensaje> estados = await _estadoMensajeRepo.ReadAll();

                result.UserMessage = $"La lista de estados de mensaje fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.EstadoMensajeService: ReadAllEstadoMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = estados;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de estados de mensaje";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoMensajeService: ReadAllEstadoMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateEstadoMensaje(int id, EstadoMensaje estadoMensaje)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoMensajeRepo.Update(id, estadoMensaje);

                result.UserMessage = state ?
                    $"El estado de mensaje { estadoMensaje.Nombre } fue actualizado corretamente" : $"El estado de mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoMensajeService: UpdateEstadoMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de mensaje { estadoMensaje.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoMensajeService: UpdateEstadoMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteEstadoMensaje(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoMensajeRepo.Delete(id);

                result.UserMessage = state ?
                    $"El estado de mensaje fue eliminado corretamente" : $"El estado de mensaje solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoMensajeService: DeleteEstadoMensaje() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de mensaje no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoMensajeService: DeleteEstadoMensaje(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<EstadoMensaje> ReadSimple(int id)
        {
            try
            {
                EstadoMensaje estado = await new EstadoMensajeRepo().Read(id);

                return estado;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<EstadoMensaje>> ReadAllSimple()
        {
            try
            {
                List<EstadoMensaje> estados = await new EstadoMensajeRepo().ReadAll();

                return estados;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
