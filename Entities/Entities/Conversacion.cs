using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Conversacion : Dates
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Mensaje> Mensajes { get; set; }
        public List<Usuario_Conversacion> Miembros { get; set; }
    }
}
