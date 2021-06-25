using Lorian_API;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lorian_API.Helpers
{
    public static class Archivo
    {
        public static async Task Guardar(string ruta, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        string filePath = ruta;
                        using (FileStream stream = File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                            await stream.FlushAsync();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static FileStream Obtener(string rutaArchivo)
        {
            try
            {
                FileStream stream = File.Open(rutaArchivo, FileMode.Open);

                return stream;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static void Eliminar(string rutaArchivo)
        {
            try
            {
                if (rutaArchivo != null && rutaArchivo.Length > 0)
                {
                    File.Delete(rutaArchivo);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static string ObtenerTipoArchivo(string fileName)
        {
            string[] lista = fileName.Split(".");
            return lista[lista.Length - 1];
        }

        public static string GenerarRutaArchivo(string nombre, string carpeta, string tipo)
        {
            nombre = $"{ nombre }_{ DateTime.Now.Ticks }";
            string folderName = Path.Combine("Resources", $"Images\\{ carpeta }");
            string filePath = $"{ folderName }\\{ nombre }.{ tipo }";
            return filePath;
        }
    }
}
