using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Envio : Dates
    {
        public int Id { get; set; }
        public int IdEstadoEnvio { get; set; }
        public int IdFactura { get; set; }
        public int IdDireccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaEntregado { get; set; }

        public EstadoEnvio Estado { get; set; }
        public Factura Factura { get; set; }
        public Direccion Direccion { get; set; }
    }
}
