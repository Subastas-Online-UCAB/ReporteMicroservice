using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Domain.Events
{
    public class ReporteEditadoEvent
    {
        public Guid ReporteId { get; set; }
        public string Estado { get; set; }
    }
}
