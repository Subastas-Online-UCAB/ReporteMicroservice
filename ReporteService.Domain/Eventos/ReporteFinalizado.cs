using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Eventos
{
    public class ReporteFinalizado
    {
        public Guid ReporteId { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
