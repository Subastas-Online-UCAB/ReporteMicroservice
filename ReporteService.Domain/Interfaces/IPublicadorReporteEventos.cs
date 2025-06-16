using ReporteService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Domain.Interfaces
{
    public interface IPublicadorReporteEventos
    {
        Task PublicarReporteCreado(ReporteCreadoEvent evento);
        Task PublicarReporteEditado(ReporteEditadoEvent evento);

    }
}
