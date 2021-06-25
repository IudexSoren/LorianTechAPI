using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class PromocionService
    {
        private readonly PromocionRepo _promocionRepo = new PromocionRepo();

        public async Task<Generic_ResultSet<bool>> CreatePromocion(Promocion promocion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                promocion.CreationDate = DateTime.UtcNow;
                promocion.ModifiedDate = DateTime.UtcNow;

                bool state = await _promocionRepo.Create(promocion);

                result.UserMessage = state ?
                    $"La promoción { promocion.Nombre } fue creada corretamente" : $"La promoción no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.PromocionService: CreatePromocion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la promoción { promocion.Nombre } no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.PromocionService: CreatePromocion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Promocion>> ReadPromocion(int id)
        {
            Generic_ResultSet<Promocion> result = new Generic_ResultSet<Promocion>();
            try
            {
                Promocion promocion = await _promocionRepo.Read(id);

                result.UserMessage = promocion != null ?
                    $"La promoción { promocion.Nombre } fue obtenida corretamente" : $"La promoción solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.PromocionService: ReadPromocion() method executed successfully";
                result.Success = true;
                result.ResultSet = promocion;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la promoción no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.PromocionService: ReadPromocion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Promocion>>> ReadAllPromocion()
        {
            Generic_ResultSet<List<Promocion>> result = new Generic_ResultSet<List<Promocion>>();
            try
            {
                List<Promocion> promocions = await _promocionRepo.ReadAll();

                result.UserMessage = $"La lista de promociones fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.PromocionService: ReadAllPromocion() method executed successfully";
                result.Success = true;
                result.ResultSet = promocions;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de promociones";
                result.InternalMessage = $"ERROR: LOGIC.Services.PromocionService: ReadAllPromocion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdatePromocion(int id, Promocion promocion)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                promocion.ModifiedDate = DateTime.UtcNow;

                bool state = await _promocionRepo.Update(id, promocion);

                result.UserMessage = state ?
                    $"La promoción { promocion.Nombre } fue actualizada corretamente" : $"La promoción solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.PromocionService: UpdatePromocion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la promoción { promocion.Nombre } no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.PromocionService: UpdatePromocion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeletePromocion(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _promocionRepo.Delete(id);

                result.UserMessage = state ?
                    $"La promoción fue eliminada corretamente" : $"La promoción solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.PromocionService: DeletePromocion() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la promoción no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.PromocionService: DeletePromocion(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
