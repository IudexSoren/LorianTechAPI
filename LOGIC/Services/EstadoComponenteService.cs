using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class EstadoComponenteService
    {
        private readonly EstadoComponenteRepo _estadoComponenteRepo = new EstadoComponenteRepo();

        public async Task<Generic_ResultSet<bool>> CreateEstadoComponente(EstadoComponente estadoComponente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoComponenteRepo.Create(estadoComponente);

                result.UserMessage = state ?
                    $"El estado de componente { estadoComponente.Nombre } fue creado corretamente" : $"El estado de componente no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.EstadoComponenteService: CreateEstadoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de componente { estadoComponente.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoComponenteService: CreateEstadoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<EstadoComponente>> ReadEstadoComponente(int id)
        {
            Generic_ResultSet<EstadoComponente> result = new Generic_ResultSet<EstadoComponente>();
            try
            {
                EstadoComponente estado = await _estadoComponenteRepo.Read(id);

                result.UserMessage = estado != null ?
                    $"El estado de componente { estado.Nombre } fue obtenido corretamente" : "El estado de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoComponenteService: ReadEstadoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = estado;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de componente no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoComponenteService: ReadEstadoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<EstadoComponente>>> ReadAllEstadoComponente()
        {
            Generic_ResultSet<List<EstadoComponente>> result = new Generic_ResultSet<List<EstadoComponente>>();
            try
            {
                List<EstadoComponente> estados = await _estadoComponenteRepo.ReadAll();

                result.UserMessage = $"La lista de estados de componente fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.EstadoComponenteService: ReadAllEstadoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = estados;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de estados de componente";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoComponenteService: ReadAllEstadoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateEstadoComponente(int id, EstadoComponente estadoComponente)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoComponenteRepo.Update(id, estadoComponente);

                result.UserMessage = state ?
                    $"El estado de componente { estadoComponente.Nombre } fue actualizado corretamente" : $"El estado de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoComponenteService: UpdateEstadoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de componente { estadoComponente.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoComponenteService: UpdateEstadoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteEstadoComponente(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoComponenteRepo.Delete(id);

                result.UserMessage = state ?
                    $"El estado de componente fue eliminado corretamente" : $"El estado de componente solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoComponenteService: DeleteEstadoComponente() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de componente no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoComponenteService: DeleteEstadoComponente(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<EstadoComponente> ReadSimple(int id)
        {
            try
            {
                EstadoComponente estado = await new EstadoComponenteRepo().Read(id);

                return estado;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<EstadoComponente>> ReadAllSimple()
        {
            try
            {
                List<EstadoComponente> estados = await new EstadoComponenteRepo().ReadAll();

                return estados;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
