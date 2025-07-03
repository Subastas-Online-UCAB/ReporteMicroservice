using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Excepciones
{
    public class ReporteNoEncontradoException : Exception
    {
        public ReporteNoEncontradoException(Guid reporteId)
            : base($"No se encontró el reporte con ID: {reporteId}") { }
    }
}
