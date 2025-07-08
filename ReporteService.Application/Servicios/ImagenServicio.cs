using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ReporteService.Application.Servicios;

public class ImagenService
{
private readonly string _rutaBase = @"C:\Users\Xrixer\Pictures\Proyecto";

public async Task<string> GuardarImagen(IFormFile imagen, Guid IdReporte)
{
    if (imagen == null || imagen.Length == 0)
        throw new ArgumentException("La imagen no es válida.");

    // Crear directorio si no existe
    Directory.CreateDirectory(_rutaBase);

    // Generar nombre único para la imagen (ej: "reporte-{id}.jpg")
    string extension = Path.GetExtension(imagen.FileName);
    string nombreArchivo = $"reporte-{IdReporte}{extension}";
    string rutaCompleta = Path.Combine(_rutaBase, nombreArchivo);

    // Guardar la imagen
    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
    {
        await imagen.CopyToAsync(stream);
    }

    return rutaCompleta; // Opcional: Puedes devolver solo el nombreArchivo o ruta relativa
}

    public void EliminarImagen(string rutaImagen)
    {
        if (File.Exists(rutaImagen))
        {
            File.Delete(rutaImagen);
        }
    }
}
