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
    public class EstadoUsuarioService
    {
        private readonly EstadoUsuarioRepo _estadoUsuarioRepo = new EstadoUsuarioRepo();

        public async Task<Generic_ResultSet<bool>> CreateEstadoUsuario(EstadoUsuario estadoUsuario)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoUsuarioRepo.Create(estadoUsuario);

                result.UserMessage = state ? 
                    $"El estado de usuario { estadoUsuario.Nombre } fue creado corretamente" : "El estado de usuario no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.EstadoUsuarioService: CreateEstadoUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de usuario { estadoUsuario.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoUsuarioService: CreateEstadoUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<EstadoUsuario>> ReadEstadoUsuario(int id)
        {
            Generic_ResultSet<EstadoUsuario> result = new Generic_ResultSet<EstadoUsuario>();
            try
            {
                EstadoUsuario estado = await _estadoUsuarioRepo.Read(id);

                result.UserMessage = estado != null ? 
                    $"El estado de usuario { estado.Nombre } fue obtenido corretamente" : "El estado de usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoUsuarioService: ReadEstadoUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = estado;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de usuario no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoUsuarioService: ReadEstadoUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<EstadoUsuario>>> ReadAllEstadoUsuario()
        {
            Generic_ResultSet<List<EstadoUsuario>> result = new Generic_ResultSet<List<EstadoUsuario>>();
            try
            {
                List<EstadoUsuario> estados = await _estadoUsuarioRepo.ReadAll();

                result.UserMessage = $"La lista de estados de usuario fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.EstadoUsuarioService: ReadAllEstadoUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = estados;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de estados de usuario";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoUsuarioService: ReadAllEstadoUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateEstadoUsuario(int id, EstadoUsuario estadoUsuario)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoUsuarioRepo.Update(id, estadoUsuario);

                result.UserMessage = state ? 
                    $"El estado de usuario { estadoUsuario.Nombre } fue actualizado corretamente" : "El estado de usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoUsuarioService: UpdateEstadoUsuario() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de usuario { estadoUsuario.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoUsuarioService: UpdateEstadoUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteEstadoUsuario(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _estadoUsuarioRepo.Delete(id);

                result.UserMessage = state ? 
                    $"El estado de usuario fue eliminado corretamente" : $"El estado de usuario solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.EstadoUsuarioService: DeleteMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el estado de usuario no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.EstadoUsuarioService: DeleteEstadoUsuario(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
