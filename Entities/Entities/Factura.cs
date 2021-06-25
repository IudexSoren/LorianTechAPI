using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Factura : Dates
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTarjeta { get; set; }
        public double Descuento { get; set; }
        public double Impuesto { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }

        public Usuario Usuario { get; set; }
        public Tarjeta Tarjeta { get; set; }
        public List<LineaDetalle> Detalles { get; set; }
    }
}
