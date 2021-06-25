using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class TipoTarjetaService
    {
        private readonly TipoTarjetaRepo _tipoTarjetaRepo = new TipoTarjetaRepo();

        public async Task<Generic_ResultSet<bool>> CreateTipoTarjeta(TipoTarjeta tipoTarjeta)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tipoTarjeta.CreationDate = DateTime.UtcNow;
                tipoTarjeta.ModifiedDate = DateTime.UtcNow;

                bool state = await _tipoTarjetaRepo.Create(tipoTarjeta);

                result.UserMessage = state ?
                    $"El tipo de tarjeta { tipoTarjeta.Nombre } fue creado corretamente" : $"El tipo de tarjeta no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.TipoTarjetaService: CreateTipoTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de tarjeta { tipoTarjeta.Nombre } no pudo ser creado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoTarjetaService: CreateTipoTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<TipoTarjeta>> ReadTipoTarjeta(int id)
        {
            Generic_ResultSet<TipoTarjeta> result = new Generic_ResultSet<TipoTarjeta>();
            try
            {
                TipoTarjeta tipo = await _tipoTarjetaRepo.Read(id);

                result.UserMessage = tipo != null ?
                    $"El tipo de tarjeta { tipo.Nombre } fue obtenido corretamente" : $"El tipo de tarjeta solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoTarjetaService: ReadTipoTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = tipo;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de tarjeta no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoTarjetaService: ReadTipoTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<TipoTarjeta>>> ReadAllTipoTarjeta()
        {
            Generic_ResultSet<List<TipoTarjeta>> result = new Generic_ResultSet<List<TipoTarjeta>>();
            try
            {
                List<TipoTarjeta> tipos = await _tipoTarjetaRepo.ReadAll();

                result.UserMessage = $"La lista de tipos de tarjeta fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.TipoTarjetaService: ReadAllTipoTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = tipos;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de tipos de tarjeta";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoTarjetaService: ReadAllTipoTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateTipoTarjeta(int id, TipoTarjeta tipoTarjeta)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                tipoTarjeta.ModifiedDate = DateTime.UtcNow;

                bool state = await _tipoTarjetaRepo.Update(id, tipoTarjeta);

                result.UserMessage = state ?
                    $"El tipo de tarjeta { tipoTarjeta.Nombre } fue obtenido corretamente" : $"El tipo de tarjeta solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoTarjetaService: UpdateTipoTarjeta() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de tarjeta { tipoTarjeta.Nombre } no pudo ser actualizado";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoTarjetaService: UpdateTipoTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteTipoTarjeta(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _tipoTarjetaRepo.Delete(id);

                result.UserMessage = state ?
                    $"El tipo de tarjeta fue eliminado corretamente" : $"El tipo de tarjeta solicitado no existe";
                result.InternalMessage = $"LOGIC.Services.TipoTarjetaService: DeleteMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que el tipo de tarjeta no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.TipoTarjetaService: DeleteTipoTarjeta(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }
    }
}
