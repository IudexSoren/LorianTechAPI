﻿using ENTITIES.Utils;
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
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public Tarjeta Tarjeta { get; set; }
        public List<LineaDetalle> Detalles { get; set; }
    }
}
