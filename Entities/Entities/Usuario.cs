using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Usuario : Dates
    {
        public int Id { get; set; }
        public int IdEstadoUsuario { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Clave { get; set; }
        public bool EmailVerificado { get; set; }

        public EstadoUsuario Estado { get; set; }
        public Rol Rol { get; set; }
    }
}
