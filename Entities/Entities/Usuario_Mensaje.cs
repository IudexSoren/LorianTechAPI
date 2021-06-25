using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES.Entities
{
    public class Usuario_Mensaje
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdMensaje { get; set; }
        public int IdTipoUsuarioMensaje { get; set; }
        public int IdEstadoMensaje { get; set; }

        public Usuario Usuario { get; set; }
        public Mensaje Mensaje { get; set; }
        public TipoUsuarioMensaje TipoUsuario { get; set; }
        public EstadoMensaje Estado { get; set; }
    }
}
