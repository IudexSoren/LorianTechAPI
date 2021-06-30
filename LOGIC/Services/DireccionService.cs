using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class DireccionService
    {
        private readonly DireccionRepo _direccionRepo = new DireccionRepo();

        public async Task<Generic_ResultSet<bool>> CreateDireccion(Direccion direccion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                direccion.CreationDate = DateTime.UtcNow;
                direccion.ModifiedDate = DateTime.UtcNow;

                bool state = await _direccionRepo.Create(direccion);

                result.UserMessage = state ?
                    $"La dirección fue creada corretamente" : $"La dirección no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.DireccionService: CreateDireccion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la dirección no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.DireccionService: CreateDireccion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Direccion>> ReadDireccion(int id)
        {
            Generic_ResultSet<Direccion> result = new Generic_ResultSet<Direccion>();
            try
            {
                Direccion direccion = await _direccionRepo.Read(id);

                result.UserMessage = direccion != null ?
                    $"La dirección fue obtenida corretamente" : $"La dirección solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.DireccionService: ReadDireccion() method executed successfully";
                result.Success = true;
                result.ResultSet = direccion;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la dirección no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.DireccionService: ReadDireccion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Direccion>>> ReadAllDireccion(int? idUsuario)
        {
            Generic_ResultSet<List<Direccion>> result = new Generic_ResultSet<List<Direccion>>();
            try
            {
                List<Direccion> direcciones = 
                    (idUsuario.HasValue) ? await _direccionRepo.ReadAllByIdUsuario(idUsuario.Value) : await _direccionRepo.ReadAll();

                result.UserMessage = $"La lista de direcciones fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.DireccionService: ReadAllDireccion() method executed successfully";
                result.Success = true;
                result.ResultSet = direcciones;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de direcciones";
                result.InternalMessage = $"ERROR: LOGIC.Services.DireccionService: ReadAllDireccion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateDireccion(int id, Direccion direccion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                direccion.ModifiedDate = DateTime.UtcNow;

                bool state = await _direccionRepo.Update(id, direccion);

                result.UserMessage = state ?
                    $"La dirección fue actualizada corretamente" : $"La dirección solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.DireccionService: UpdateDireccion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la dirección no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.DireccionService: UpdateDireccion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteDireccion(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _direccionRepo.Delete(id);

                result.UserMessage = state ?
                    $"La dirección fue eliminada corretamente" : $"La dirección solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.DireccionService: DeleteDireccion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la dirección no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.DireccionService: DeleteDireccion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Direccion> ReadSimple(int id)
        {
            try
            {
                Direccion direccion = await new DireccionRepo().Read(id);

                return direccion;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Direccion>> ReadAllSimple()
        {
            try
            {
                List<Direccion> direccions = await new DireccionRepo().ReadAll();

                return direccions;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
