using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Domain.Eventos
{
    public class ReporteEditadoEvento
    {
        public Guid ReporteId { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }
    }
}
