using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Eventos
{
    public class ReporteCreadoEvento
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdSubasta { get; set; }
    }
}
