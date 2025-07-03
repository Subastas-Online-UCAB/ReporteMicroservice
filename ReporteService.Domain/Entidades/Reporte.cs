using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Entidades
{
    public class Reporte
    {
        public Guid IdReporte { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdSubasta { get; set; }



        public void Editar(string titulo, string descripcion, string estado)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Estado = estado;

        }
    }
    public enum EstadoReporte
        {
            Pendiente,
            EnCurso,
            Finalizado
        }

}
