using DAL.Repositories;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public static class ElementExistsService
    {
        public static async Task<Caracteristica_Componente> Caracteristica_ComponenteExists(int id)
        {
            var result = await new Caracteristica_ComponenteRepo().Read(id);

            return result;
        }
        
        public static async Task<Caracteristica_Componente> Caracteristica_ComponenteExists(int idCaracteristica, int idComponente)
        {
            var result = await new Caracteristica_ComponenteRepo().ReadByIds(idComponente, idCaracteristica);

            return result;
        }

        public static async Task<Promocion_Componente> Promocion_ComponenteExists(int id)
        {
            var result = await new Promocion_ComponenteRepo().Read(id);

            return result;
        }

        public static async Task<Promocion_Componente> Promocion_ComponenteExists(int idPromocion, int idComponente)
        {
            var result = await new Promocion_ComponenteRepo().ReadByIds(idComponente, idPromocion);

            return result;
        }
    }
}
