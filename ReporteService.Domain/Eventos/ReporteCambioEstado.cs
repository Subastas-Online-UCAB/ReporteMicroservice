using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Eventos
{
    public class ReporteCambioEstado
    {
        public Guid ReporteId { get; set; }
        public string NuevoEstado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

}
