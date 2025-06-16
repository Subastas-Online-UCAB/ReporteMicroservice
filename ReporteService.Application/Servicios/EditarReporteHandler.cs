using MediatR;
using ReporteService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporteService.Domain.Events;
using ReporteService.Application.Comun;
using ReporteService.Domain.Entidades;
using ReporteService.Domain.Repositorios;
using ReporteService.Domain.Interfaces;

namespace ReporteService.Application.Servicios
{
    public class EditarReporteHandler : IRequestHandler<EditarReporteCommand, MessageResponse>
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly IPublicadorReporteEventos _eventPublisher;

        public EditarReporteHandler(IReporteRepository reporteRepository, IPublicadorReporteEventos eventPublisher)
        {
            _reporteRepository = reporteRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<MessageResponse> Handle(EditarReporteCommand request, CancellationToken cancellationToken)
        {
            var reporte = await _reporteRepository.ObtenerPorIdAsync(request.ReporteId, cancellationToken);
            if (reporte == null)
                return MessageResponse.CrearError("El reporte no existe.");

            if (reporte.IdUsuario != request.UsuarioId)
                return MessageResponse.CrearError("No tienes permiso para modificar este reporte.");

            reporte.Editar(request.Titulo, request.Descripcion, request.Estado);
            await _reporteRepository.ActualizarAsync(reporte, cancellationToken);

            await _eventPublisher.PublicarReporteEditado( new ReporteEditadoEvent
            {
                ReporteId = reporte.IdReporte,
                Estado = reporte.Estado,
            });

            return MessageResponse.CrearExito("Reporte editado exitosamente.");
        }
    }
}
