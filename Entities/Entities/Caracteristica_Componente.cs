using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Caracteristica_Componente
    {
        public int Id { get; set; }
        public int IdComponente { get; set; }
        public int IdCaracteristica { get; set; }

        public Componente Componente { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }
}
