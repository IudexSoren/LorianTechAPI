using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class RolService
    {
        private readonly RolRepo _rolRepo = new RolRepo();

        public async Task<Generic_ResultSet<bool>> CreateRol(Rol rol)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                rol.CreationDate = DateTime.UtcNow;
                rol.ModifiedDate = DateTime.UtcNow;

                bool state = await _rolRepo.Create(rol);

                result.UserMessage = state ?
                    $"El rol { rol.Nombre } fue creado corretamente" : $"El rol no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.RolService: CreateRol() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el rol { rol.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.RolService: CreateRol(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Rol>> ReadRol(int id)
        {
            Generic_ResultSet<Rol> result = new Generic_ResultSet<Rol>();
            try
            {
                Rol rol = await _rolRepo.Read(id);

                result.UserMessage = rol != null ?
                    $"El rol { rol.Nombre } fue obtenido corretamente" : $"El rol solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.RolService: ReadRol() method executed successfully";
                result.Success = true;
                result.ResultSet = rol;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el rol no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.RolService: ReadRol(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Rol>>> ReadAllRol()
        {
            Generic_ResultSet<List<Rol>> result = new Generic_ResultSet<List<Rol>>();
            try
            {
                List<Rol> rols = await _rolRepo.ReadAll();

                result.UserMessage = $"La lista de roles fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.RolService: ReadAllRol() method executed successfully";
                result.Success = true;
                result.ResultSet = rols;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de roles";
                result.InternalMessage = $"ERROR: LOGIC.Services.RolService: ReadAllRol(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateRol(int id, Rol rol)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                rol.ModifiedDate = DateTime.UtcNow;

                bool state = await _rolRepo.Update(id, rol);

                result.UserMessage = state ?
                    $"El rol { rol.Nombre } fue actualizado corretamente" : $"El rol solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.RolService: UpdateRol() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el rol { rol.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.RolService: UpdateRol(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteRol(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _rolRepo.Delete(id);

                result.UserMessage = state ?
                    $"El rol fue eliminado corretamente" : $"El rol solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.RolService: DeleteRol() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el rol no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.RolService: DeleteRol(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
