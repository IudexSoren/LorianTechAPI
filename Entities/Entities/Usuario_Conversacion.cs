using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Usuario_Conversacion : Dates
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdConversacion { get; set; }
        public bool IsAdministrador { get; set; }

        public Usuario Usuario { get; set; }
        public Conversacion Conversacion { get; set; }
    }
}
