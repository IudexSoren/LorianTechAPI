using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class TelefonoService
    {
        private readonly TelefonoRepo _telefonoRepo = new TelefonoRepo();

        public async Task<Generic_ResultSet<bool>> CreateTelefono(Telefono telefono)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                telefono.CreationDate = DateTime.UtcNow;
                telefono.ModifiedDate = DateTime.UtcNow;

                bool state = await _telefonoRepo.Create(telefono);

                result.UserMessage = state ?
                    $"El teléfono { telefono.Numero } fue creado corretamente" : $"El teléfono no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.TelefonoService: CreateTelefono() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el teléfono { telefono.Numero } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TelefonoService: CreateTelefono(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Telefono>> ReadTelefono(int id)
        {
            Generic_ResultSet<Telefono> result = new Generic_ResultSet<Telefono>();
            try
            {
                Telefono telefono = await _telefonoRepo.Read(id);

                result.UserMessage = telefono != null ?
                    $"El teléfono { telefono.Numero } fue obtenido corretamente" : $"El teléfono solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TelefonoService: ReadTelefono() method executed successfully";
                result.Success = true;
                result.ResultSet = telefono;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el teléfono no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TelefonoService: ReadTelefono(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Telefono>>> ReadAllTelefono(int? idUsuario)
        {
            Generic_ResultSet<List<Telefono>> result = new Generic_ResultSet<List<Telefono>>();
            try
            {
                List<Telefono> telefonoes =
                    (idUsuario.HasValue) ? await _telefonoRepo.ReadAllByIdUsuario(idUsuario.Value) : await _telefonoRepo.ReadAll();

                result.UserMessage = $"La lista de telefonos fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.TelefonoService: ReadAllTelefono() method executed successfully";
                result.Success = true;
                result.ResultSet = telefonoes;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de teléfonos";
                result.InternalMessage = $"ERROR: LOGIC.Services.TelefonoService: ReadAllTelefono(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateTelefono(int id, Telefono telefono)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                telefono.ModifiedDate = DateTime.UtcNow;

                bool state = await _telefonoRepo.Update(id, telefono);

                result.UserMessage = state ?
                    $"El teléfono { telefono.Numero } fue actualizado corretamente" : $"El teléfono solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TelefonoService: UpdateTelefono() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el teléfono { telefono.Numero } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TelefonoService: UpdateTelefono(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteTelefono(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _telefonoRepo.Delete(id);

                result.UserMessage = state ?
                    $"El teléfono fue eliminado corretamente" : $"El teléfono solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TelefonoService: DeleteTelefono() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el teléfono no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TelefonoService: DeleteTelefono(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Telefono> ReadSimple(int id)
        {
            try
            {
                Telefono telefono = await new TelefonoRepo().Read(id);

                return telefono;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Telefono>> ReadAllSimple()
        {
            try
            {
                List<Telefono> telefonos = await new TelefonoRepo().ReadAll();

                return telefonos;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
