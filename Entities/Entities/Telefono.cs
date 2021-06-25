using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Telefono : Dates
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Numero { get; set; }

        public Usuario Usuario { get; set; }
    }
}
