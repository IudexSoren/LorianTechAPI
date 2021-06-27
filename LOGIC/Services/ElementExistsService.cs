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
        public static async Task<Caracteristica> CaracteristicaExists(int id)
        {
            var result = await new CaracteristicaRepo().Read(id);

            return result;
        }

        public static async Task<Caracteristica_Componente> Caracteristica_ComponenteExists(int idCaracteristica, int idComponente)
        {
            var result = await new Caracteristica_ComponenteRepo().ReadByIds(idComponente, idCaracteristica);

            return result;
        }

        public static async Task<Componente> ComponenteExists(int id)
        {
            var result = await new ComponenteRepo().Read(id);

            return result;
        }

        public static async Task<EstadoComponente> EstadoComponenteExists(int id)
        {
            var result = await new EstadoComponenteRepo().Read(id);

            return result;
        }

        public static async Task<EstadoMensaje> EstadoMensajeExists(int id)
        {
            var result = await new EstadoMensajeRepo().Read(id);

            return result;
        }

        public static async Task<Marca> MarcaExists(int id)
        {
            var result = await new MarcaRepo().Read(id);

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

        public static async Task<Promocion> PromocionExists(int id)
        {
            var result = await new PromocionRepo().Read(id);

            return result;
        }

        public static async Task<TipoComponente> TipoComponenteExists(int id)
        {
            var result = await new TipoComponenteRepo().Read(id);

            return result;
        }

        public static async Task<TipoTarjeta> TipoTarjetaExists(int id)
        {
            var result = await new TipoTarjetaRepo().Read(id);

            return result;
        }

        public static async Task<Usuario> UsuarioExists(int id)
        {
            var result = await new UsuarioRepo().Read(id);

            return result;
        }
    }
}
