using DAL.Repositories;
using ENTITIES.Entities;
using LOGIC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class CaracteristicaService
    {
        private readonly CaracteristicaRepo _caracteristicaRepo = new CaracteristicaRepo();

        public async Task<Generic_ResultSet<bool>> CreateCaracteristica(Caracteristica caracteristica)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                caracteristica.CreationDate = DateTime.UtcNow;
                caracteristica.ModifiedDate = DateTime.UtcNow;

                bool state = await _caracteristicaRepo.Create(caracteristica);

                result.UserMessage = state ?
                    $"La característica { caracteristica.Nombre } fue creada corretamente" : $"La característica no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.CaracteristicaService: CreateCaracteristica() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la característica { caracteristica.Nombre } no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.CaracteristicaService: CreateCaracteristica(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Caracteristica>> ReadCaracteristica(int id)
        {
            Generic_ResultSet<Caracteristica> result = new Generic_ResultSet<Caracteristica>();
            try
            {
                Caracteristica caracteristica = await _caracteristicaRepo.Read(id);

                result.UserMessage = caracteristica != null ?
                    $"La característica { caracteristica.Nombre } fue obtenida corretamente" : $"La característica solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.CaracteristicaService: ReadCaracteristica() method executed successfully";
                result.Success = true;
                result.ResultSet = caracteristica;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la característica no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.CaracteristicaService: ReadCaracteristica(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Caracteristica>>> ReadAllCaracteristica()
        {
            Generic_ResultSet<List<Caracteristica>> result = new Generic_ResultSet<List<Caracteristica>>();
            try
            {
                List<Caracteristica> caracteristicas = await _caracteristicaRepo.ReadAll();

                result.UserMessage = $"La lista de características fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.CaracteristicaService: ReadAllCaracteristica() method executed successfully";
                result.Success = true;
                result.ResultSet = caracteristicas;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de características";
                result.InternalMessage = $"ERROR: LOGIC.Services.CaracteristicaService: ReadAllCaracteristica(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateCaracteristica(int id, Caracteristica caracteristica)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                caracteristica.ModifiedDate = DateTime.UtcNow;

                bool state = await _caracteristicaRepo.Update(id, caracteristica);

                result.UserMessage = state ?
                    $"La característica { caracteristica.Nombre } fue actualizada corretamente" : $"La característica solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.CaracteristicaService: UpdateCaracteristica() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la característica { caracteristica.Nombre } no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.CaracteristicaService: UpdateCaracteristica(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteCaracteristica(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _caracteristicaRepo.Delete(id);

                result.UserMessage = state ?
                    $"La característica fue eliminada corretamente" : $"La característica solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.CaracteristicaService: DeleteCaracteristica() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la característica no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.CaracteristicaService: DeleteCaracteristica(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Caracteristica> ReadSimple(int id)
        {
            try
            {
                Caracteristica caracteristica = await new CaracteristicaRepo().Read(id);

                return caracteristica;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Caracteristica>> ReadAllSimple()
        {
            try
            {
                List<Caracteristica> caracteristicas = await new CaracteristicaRepo().ReadAll();

                return caracteristicas;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
