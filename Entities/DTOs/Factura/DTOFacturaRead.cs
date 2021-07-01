using ENTITIES.Entities;
using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOFacturaRead : Dates
    {
        public int Id { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DTOUsuarioRead Usuario { get; set; }
        public Tarjeta Tarjeta { get; set; }
        public List<DTOLineaDetalleRead> Detalles { get; set; }
    }
}
