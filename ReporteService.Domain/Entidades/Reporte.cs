using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReporteService.Domain.Entidades
{
    public class Reporte
    {
        public Guid IdReporte { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public Guid IdUsuario { get; set; }

    

    public void Editar(string titulo, string descripcion, string estado)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Estado = estado;

        }
    }
    public enum EstadoReporte
        {
            Borrador,
            EnCurso,
            Finalizado
        }

}
