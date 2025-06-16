using MediatR;
using ReporteService.Application.Commands;
using ReporteService.Domain.Entidades;
using ReporteService.Domain.Repositorios;
using ReporteService.Domain.Interfaces;
using ReporteService.Domain.Events;

namespace ReporteService.Application.Servicios
{
    public class CrearReporteCommandHandler : IRequestHandler<CrearReporteCommand, Guid>
    {
        private readonly IReporteRepository _repository;
        private readonly IPublicadorReporteEventos _publisher;

        public CrearReporteCommandHandler(IReporteRepository repository, IPublicadorReporteEventos publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CrearReporteCommand request, CancellationToken cancellationToken)
        {
            var reporte = new Reporte
            {
                IdReporte = Guid.NewGuid(),
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Estado = "Pendiente",
                IdUsuario = request.IdUsuario,
            };

            // 1. Persistencia en base de datos principal (PostgreSQL)
            await _repository.CrearAsync(reporte, cancellationToken);

            // 2. Publicar evento general (por ejemplo, para vistas o proyecciones)
            var eventoCreado = new ReporteCreadoEvent
            {
                Id = reporte.IdReporte,
                Titulo = reporte.Titulo,
                Descripcion = reporte.Descripcion,
                Estado = "Pendiente",
                IdUsuario = reporte.IdUsuario,
            };

            await _publisher.PublicarReporteCreado(eventoCreado);

            return reporte.IdReporte;
        }
    }
}
