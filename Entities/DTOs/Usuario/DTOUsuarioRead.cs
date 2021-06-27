using ENTITIES.Entities;
using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.DTOs
{
    public class DTOUsuarioRead : Dates
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int EmailVerificado { get; set; }
        public int IdEstadoUsuario { get; set; }
        public int IdRol { get; set; }
        public string Token { get; set; }
        public EstadoUsuario EstadoUsuario { get; set; }
        public Rol Rol { get; set; }
    }
}
