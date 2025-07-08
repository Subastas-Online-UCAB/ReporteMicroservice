using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporteService.Dominio.Entidades;

namespace ReporteService.Dominio.Repositorios
{
    public interface IReporteRepository
    {
        Task<Guid> CrearAsync(Reporte reporte, CancellationToken cancellationToken);

        Task<Reporte?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task ActualizarAsync(Reporte reporte, CancellationToken cancellationToken);
        Task CambiarEstadoAsync(Guid idReporte, Guid idUsuario, CancellationToken cancellationToken);

        Task<Reporte?> ObtenerReportePorIdAsync(Guid id, CancellationToken cancellationToken);

        Task EliminarReporteAsync(Guid idReporte, Guid idUsuario, CancellationToken cancellationToken);



    }
}
