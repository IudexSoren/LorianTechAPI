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
    public class EstadoEnvioService
    {
        private readonly EstadoEnvioRepo _estadoEnvioRepo = new EstadoEnvioRepo();

        public async Task<Generic_ResultSet<bool>> CreateEstadoEnvio(EstadoEnvio estadoEnvio)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoEnvioRepo.Create(estadoEnvio);

                result.UserMessage = state ?
                    $"El estado de envío { estadoEnvio.Nombre } fue creado corretamente" : $"El estado de envío no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.EstadoEnvioService: CreateEstadoEnvio() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de envío { estadoEnvio.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoEnvioService: CreateEstadoEnvio(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<EstadoEnvio>> ReadEstadoEnvio(int id)
        {
            Generic_ResultSet<EstadoEnvio> result = new Generic_ResultSet<EstadoEnvio>();
            try
            {
                EstadoEnvio estado = await _estadoEnvioRepo.Read(id);

                result.UserMessage = estado != null ?
                    $"El estado de envío { estado.Nombre } fue obtenido corretamente" : "El estado de envío solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoEnvioService: ReadEstadoEnvio() method executed successfully";
                result.Success = true;
                result.ResultSet = estado;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de envío no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoEnvioService: ReadEstadoEnvio(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<EstadoEnvio>>> ReadAllEstadoEnvio()
        {
            Generic_ResultSet<List<EstadoEnvio>> result = new Generic_ResultSet<List<EstadoEnvio>>();
            try
            {
                List<EstadoEnvio> estados = await _estadoEnvioRepo.ReadAll();

                result.UserMessage = $"La lista de estados de envío fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.EstadoEnvioService: ReadAllEstadoEnvio() method executed successfully";
                result.Success = true;
                result.ResultSet = estados;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de estados de envío";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoEnvioService: ReadAllEstadoEnvio(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateEstadoEnvio(int id, EstadoEnvio estadoEnvio)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoEnvioRepo.Update(id, estadoEnvio);

                result.UserMessage = state ? 
                    $"El estado de envío { estadoEnvio.Nombre } fue actualizado corretamente" : $"El estado de envío solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoEnvioService: UpdateEstadoEnvio() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de envío { estadoEnvio.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoEnvioService: UpdateEstadoEnvio(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteEstadoEnvio(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoEnvioRepo.Delete(id);

                result.UserMessage = state ? 
                    $"El estado de envío fue eliminado corretamente" : $"El estado de envío solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoEnvioService: DeleteEstadoEnvio() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de envío no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoEnvioService: DeleteEstadoEnvio(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<EstadoEnvio> ReadSimple(int id)
        {
            try
            {
                EstadoEnvio estado = await new EstadoEnvioRepo().Read(id);

                return estado;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<EstadoEnvio>> ReadAllSimple()
        {
            try
            {
                List<EstadoEnvio> estados = await new EstadoEnvioRepo().ReadAll();

                return estados;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
