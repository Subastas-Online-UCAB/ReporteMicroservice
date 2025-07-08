using MediatR;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;
using ReporteService.Dominio.Interfaces;
using ReporteService.Dominio.Eventos;
using ReporteService.Application.Servicios;

namespace ReporteService.Application.Servicios
{
    public class CrearReporteCommandHandler : IRequestHandler<CrearReporteCommand, Guid>
    {
        private readonly IReporteRepository _repository;
        private readonly IPublicadorReporteEventos _publisher;
        private readonly ImagenService _imagenService;

        public CrearReporteCommandHandler(IReporteRepository repository, IPublicadorReporteEventos publisher, ImagenService imagenService)
        {
            _repository = repository;
            _publisher = publisher;
            _imagenService = imagenService;
        }

        public async Task<Guid> Handle(CrearReporteCommand request, CancellationToken cancellationToken)
        {
            // 1. Guardar la imagen en el sistema de archivos
            string rutaImagen = await _imagenService.GuardarImagen(request.Imagen, Guid.NewGuid());

            var reporte = new Reporte
            {
                IdReporte = Guid.NewGuid(),
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Estado = "Pendiente",
                FechaCreacion = request.FechaCreacion,
                ImagenRuta = rutaImagen,
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
                ImagenRuta = reporte.ImagenRuta,
                IdUsuario = reporte.IdUsuario,
                IdSubasta = reporte.IdSubasta,  
            };

            await _publisher.PublicarReporteCreado(eventoCreado);

            return reporte.IdReporte;
        }
    }
}
