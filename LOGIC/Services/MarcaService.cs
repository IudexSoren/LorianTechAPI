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
    public class MarcaService
    {
        private readonly MarcaRepo _marcaRepo = new MarcaRepo();

        public async Task<Generic_ResultSet<bool>> CreateMarca(Marca marca)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                marca.CreationDate = DateTime.UtcNow;
                marca.ModifiedDate = DateTime.UtcNow;

                bool state = await _marcaRepo.Create(marca);

                result.UserMessage = state ? 
                    $"La marca { marca.Nombre } fue creada corretamente" : $"La marca no pudo crearse";
                result.InternalMessage = $"LOGIC.Services.MarcaService: CreateMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la marca { marca.Nombre } no pudo ser creada";
                result.InternalMessage = $"ERROR: LOGIC.Services.MarcaService: CreateMarca(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<Marca>> ReadMarca(int id)
        {
            Generic_ResultSet<Marca> result = new Generic_ResultSet<Marca>();
            try
            {
                Marca marca = await _marcaRepo.Read(id);

                result.UserMessage = marca != null ? 
                    $"La marca { marca.Nombre } fue obtenida corretamente" : $"La marca solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.MarcaService: ReadMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = marca;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la marca no pudo obtenerse";
                result.InternalMessage = $"ERROR: LOGIC.Services.MarcaService: ReadMarca(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<List<Marca>>> ReadAllMarca()
        {
            Generic_ResultSet<List<Marca>> result = new Generic_ResultSet<List<Marca>>();
            try
            {
                List<Marca> marcas = await _marcaRepo.ReadAll();

                result.UserMessage = $"La lista de marcas fue obtenida corretamente";
                result.InternalMessage = $"LOGIC.Services.MarcaService: ReadAllMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = marcas;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que no pudo obtenerse la lista de marcas";
                result.InternalMessage = $"ERROR: LOGIC.Services.MarcaService: ReadAllMarca(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> UpdateMarca(int id, Marca marca)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                marca.ModifiedDate = DateTime.UtcNow;

                bool state = await _marcaRepo.Update(id, marca);

                result.UserMessage = state ? 
                    $"La marca { marca.Nombre } fue actualizada corretamente" : $"La marca solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.MarcaService: UpdateMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la marca { marca.Nombre } no pudo ser actualizada";
                result.InternalMessage = $"ERROR: LOGIC.Services.MarcaService: UpdateMarca(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async Task<Generic_ResultSet<bool>> DeleteMarca(int id)
        {
            Generic_ResultSet<bool> result = new Generic_ResultSet<bool>();
            try
            {
                bool state = await _marcaRepo.Delete(id);

                result.UserMessage = state ? 
                    $"La marca fue eliminada corretamente" : $"La marca solicitada no existe";
                result.InternalMessage = $"LOGIC.Services.MarcaService: DeleteMarca() method executed successfully";
                result.Success = true;
                result.ResultSet = state;
            }
            catch (Exception exception)
            {
                result.UserMessage = $"Ocurrió un error, por lo que la marca no pudo eliminarse";
                result.InternalMessage = $"ERROR: LOGIC.Services.MarcaService: DeleteMarca(): { exception.Message }";
                result.Exception = exception;
            }

            return result;
        }

        public async static Task<Marca> ReadSimple(int id)
        {
            try
            {
                Marca marca = await new MarcaRepo().Read(id);

                return marca;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async static Task<List<Marca>> ReadAllSimple()
        {
            try
            {
                List<Marca> marcas = await new MarcaRepo().ReadAll();

                return marcas;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
