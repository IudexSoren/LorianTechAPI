using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOUsuarioUpdate
    {
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ClaveActual { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool EmailVerificado { get; set; }
        public int IdEstadoUsuario { get; set; }
    }
}
