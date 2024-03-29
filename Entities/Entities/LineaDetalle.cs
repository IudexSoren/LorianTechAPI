﻿using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class LineaDetalle
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdComponente { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public Componente Componente { get; set; }
    }
}
