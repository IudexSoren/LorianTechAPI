using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class TarjetaService
    {
        private readonly TarjetaRepo _tarjetaRepo = new TarjetaRepo();

        public async Task<Generic_ResultSet<bool>> CreateTarjeta(Tarjeta tarjeta)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tarjeta.CreationDate = DateTime.UtcNow;
                tarjeta.ModifiedDate = DateTime.UtcNow;

                bool state = await _tarjetaRepo.Create(tarjeta);

                result.UserMessage = state ?
                    $"La tarjeta fue creada corretamente" : $"La tarjeta no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.TarjetaService: CreateTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la tarjeta no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.TarjetaService: CreateTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Tarjeta>> ReadTarjeta(int id)
        {
            Generic_ResultSet<Tarjeta> result = new Generic_ResultSet<Tarjeta>();
            try
            {
                Tarjeta tarjeta = await _tarjetaRepo.Read(id);
                if (tarjeta != null)
                {
                    tarjeta.TipoTarjeta = await TipoTarjetaService.ReadSimple(tarjeta.IdTipoTarjeta);
                }

                result.UserMessage = tarjeta != null ?
                    $"La tarjeta fue obtenida corretamente" : $"La tarjeta solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.TarjetaService: ReadTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = tarjeta;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la tarjeta no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TarjetaService: ReadTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Tarjeta>>> ReadAllTarjeta(int? idUsuario)
        {
            Generic_ResultSet<List<Tarjeta>> result = new Generic_ResultSet<List<Tarjeta>>();
            try
            {
                List<Tarjeta> tarjetas =
                    (idUsuario.HasValue) ? await _tarjetaRepo.ReadAllByIdUsuario(idUsuario.Value) : await _tarjetaRepo.ReadAll();

                foreach (var tarjeta in tarjetas)
                {
                    tarjeta.TipoTarjeta = await TipoTarjetaService.ReadSimple(tarjeta.IdTipoTarjeta);
                }

                result.UserMessage = $"La lista de tarjetas fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.TarjetaService: ReadAllTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = tarjetas;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de tarjetas";
                result.InternalMessage = $"ERROR: LOGIC.Services.TarjetaService: ReadAllTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateTarjeta(int id, Tarjeta tarjeta)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tarjeta.ModifiedDate = DateTime.UtcNow;

                bool state = await _tarjetaRepo.Update(id, tarjeta);

                result.UserMessage = state ?
                    $"La tarjeta fue actualizada corretamente" : $"La tarjeta solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.TarjetaService: UpdateTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la tarjeta no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.TarjetaService: UpdateTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteTarjeta(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tarjetaRepo.Delete(id);

                result.UserMessage = state ?
                    $"La tarjeta fue eliminada corretamente" : $"La tarjeta solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.TarjetaService: DeleteTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la tarjeta no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TarjetaService: DeleteTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Tarjeta> ReadSimple(int id)
        {
            try
            {
                Tarjeta tarjeta = await new TarjetaRepo().Read(id);

                return tarjeta;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Tarjeta>> ReadAllSimple()
        {
            try
            {
                List<Tarjeta> tarjetas = await new TarjetaRepo().ReadAll();

                return tarjetas;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
