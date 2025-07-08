using ReporteService.Domain.Eventos;
using ReporteService.Dominio.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Interfaces
{
    public interface IPublicadorReporteEventos
    {
        Task PublicarReporteCreado(ReporteCreadoEvento evento);
        Task PublicarReporteEditadoEvento(ReporteEditadoEvento evento);

    }
}
