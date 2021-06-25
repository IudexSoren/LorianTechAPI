using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Componente : Dates
    {
        public int Id { get; set; }
        public int IdTipoComponente { get; set; }
        public int IdMarca { get; set; }
        public int IdEstadoComponente { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public int Inventario { get; set; }
        public string Garantia { get; set; }
        public double Precio { get; set; }

        public TipoComponente Tipo { get; set; }
        public Marca Marca { get; set; }
        public EstadoComponente Estado { get; set; }
    }
}
