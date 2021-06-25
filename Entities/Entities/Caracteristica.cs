using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Caracteristica : Dates
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}
