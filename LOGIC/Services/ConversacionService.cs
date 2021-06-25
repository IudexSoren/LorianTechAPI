using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class ConversacionService
    {
        private readonly ConversacionRepo _conversacionRepo = new ConversacionRepo();

        public async Task<Generic_ResultSet<bool>> CreateConversacion(Conversacion conversacion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                conversacion.CreationDate = DateTime.UtcNow;
                conversacion.ModifiedDate = DateTime.UtcNow;

                bool state = await _conversacionRepo.Create(conversacion);

                result.UserMessage = state ?
                    $"La conversación { conversacion.Nombre } fue creada corretamente" : $"La conversación no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.ConversacionService: CreateConversacion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la conversación { conversacion.Nombre } no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.ConversacionService: CreateConversacion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Conversacion>> ReadConversacion(int id)
        {
            Generic_ResultSet<Conversacion> result = new Generic_ResultSet<Conversacion>();
            try
            {
                Conversacion conversacion = await _conversacionRepo.Read(id);

                result.UserMessage = conversacion != null ?
                    $"La conversación { conversacion.Nombre } fue obtenida corretamente" : $"La conversación solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.ConversacionService: ReadConversacion() method executed successfully";
                result.Success = true;
                result.ResultSet = conversacion;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la conversación no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.ConversacionService: ReadConversacion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Conversacion>>> ReadAllConversacion()
        {
            Generic_ResultSet<List<Conversacion>> result = new Generic_ResultSet<List<Conversacion>>();
            try
            {
                List<Conversacion> conversacions = await _conversacionRepo.ReadAll();

                result.UserMessage = $"La lista de características fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.ConversacionService: ReadAllConversacion() method executed successfully";
                result.Success = true;
                result.ResultSet = conversacions;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de características";
                result.InternalMessage = $"ERROR: LOGIC.Services.ConversacionService: ReadAllConversacion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateConversacion(int id, Conversacion conversacion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                conversacion.ModifiedDate = DateTime.UtcNow;

                bool state = await _conversacionRepo.Update(id, conversacion);

                result.UserMessage = state ?
                    $"La conversación { conversacion.Nombre } fue actualizada corretamente" : $"La conversación solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.ConversacionService: UpdateConversacion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la conversación { conversacion.Nombre } no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.ConversacionService: UpdateConversacion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteConversacion(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _conversacionRepo.Delete(id);

                result.UserMessage = state ?
                    $"La conversación fue eliminada corretamente" : $"La conversación solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.ConversacionService: DeleteConversacion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la conversación no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.ConversacionService: DeleteConversacion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
