using MediatR;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;
using ReporteService.Dominio.Interfaces;
using ReporteService.Dominio.Eventos;

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
                FechaCreacion = request.FechaCreacion,
                IdUsuario = request.IdUsuario,
                IdSubasta = request.IdSubasta,  
            };

            // 1. Persistencia en base de datos principal (PostgreSQL)
            await _repository.CrearAsync(reporte, cancellationToken);

            // 2. Publicar evento general (por ejemplo, para vistas o proyecciones)
            var eventoCreado = new ReporteCreadoEvento
            {
                Id = reporte.IdReporte,
                Titulo = reporte.Titulo,
                Descripcion = reporte.Descripcion,
                Estado = "Pendiente",
                FechaCreacion = reporte.FechaCreacion,
                IdUsuario = reporte.IdUsuario,
                IdSubasta = reporte.IdSubasta,  
            };

            await _publisher.PublicarReporteCreado(eventoCreado);

            return reporte.IdReporte;
        }
    }
}
