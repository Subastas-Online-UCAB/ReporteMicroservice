using MediatR;
using ReporteService.Application.DTO;
using ReporteService.Application.Queries;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;

namespace ReporteService.Application.Handlers
{
    public class ConsultarReportePorIdQueryHandler : IRequestHandler<ConsultarReportePorIdQuery, ReporteCompletoDto?>
    {
        private readonly IReporteRepository _repository;

        public ConsultarReportePorIdQueryHandler(IReporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReporteCompletoDto?> Handle(ConsultarReportePorIdQuery request, CancellationToken cancellationToken)
        {
            var reporte = await _repository.ObtenerPorIdAsync(request.IdReporte, cancellationToken);

            if (reporte == null)
                return null;

            return new ReporteCompletoDto
            {
                Id = reporte.IdReporte,
                Titulo = reporte.Titulo,
                Descripcion = reporte.Descripcion,
                Estado = reporte.Estado,
                IdUsuario = reporte.IdUsuario,
            };
        }

    }
}