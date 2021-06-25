using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Tarjeta : Dates
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoTarjeta { get; set; }
        public string Numero { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string CVV { get; set; }

        public Usuario Usuario { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }
    }
}
