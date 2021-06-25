using ENTITIES.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ENTITIES.Entities
{
    public class Marca : Dates
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
    }
}
