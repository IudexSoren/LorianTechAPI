using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOLineaDetalleCreate
    {
        public int IdComponente { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
