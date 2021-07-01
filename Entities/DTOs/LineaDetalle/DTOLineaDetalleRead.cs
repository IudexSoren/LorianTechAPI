using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOLineaDetalleRead
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        // Precio del componente al momento de la compra
        public decimal PrecioComponente { get; set; }
        public Componente Componente { get; set; }
    }
}
