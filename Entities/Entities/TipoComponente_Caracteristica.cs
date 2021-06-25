using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class TipoComponente_Caracteristica
    {
        public int Id { get; set; }
        public int IdTipoComponente { get; set; }
        public int IdCaracteristica { get; set; }

        public TipoComponente TipoComponente { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }
}
