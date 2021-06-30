using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOTarjetaUpdate
    {
        public int IdUsuario { get; set; }
        public int IdTipoTarjeta { get; set; }
        public string Numero { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string CVV { get; set; }
    }
}
