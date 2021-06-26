using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENTITIES.DTOs
{
    public class DTOComponenteCreate
    {
        public string Nombre { get; set; }
        public int Inventario { get; set; }
        public string Garantia { get; set; }
        public decimal Precio { get; set; }
        public int IdTipoComponente { get; set; }
        public int IdMarca { get; set; }
        public int IdEstadoComponente { get; set; }
        public List<int> IdsCaracteristicas { get; set; }
        public List<int> IdsPromociones { get; set; }
    }
}
