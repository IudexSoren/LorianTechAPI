using ENTITIES.Entities;
using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOComponenteRead : Dates
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public int Inventario { get; set; }
        public string Garantia { get; set; }
        public decimal Precio { get; set; }
        public TipoComponente TipoComponente { get; set; }
        public Marca Marca { get; set; }
        public EstadoComponente EstadoComponente { get; set; }
        public List<Caracteristica> Caracteristicas { get; set; }
        public List<Promocion> Promociones { get; set; }
    }
}
