using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOFacturaCreate
    {
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
        public int IdTarjeta { get; set; }
        public List<DTOLineaDetalleCreate> Detalles { get; set; }
    }
}
