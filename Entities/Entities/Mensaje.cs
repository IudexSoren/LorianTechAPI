using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Mensaje : Dates
    {
        public int Id { get; set; }
        public int IdConversacion { get; set; }
        public string Contenido { get; set; }

        public Conversacion Conversacion { get; set; }
    }
}
