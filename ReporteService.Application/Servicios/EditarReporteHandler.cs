using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Eventos;
using ReporteService.Application.Comun;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;
using ReporteService.Dominio.Interfaces;
using ReporteService.Domain.Eventos;

namespace ReporteService.Application.Servicios;

public class EditarReporteHandler : IRequestHandler<EditarReporteCommand, MessageResponse>
{
    private readonly IReporteRepository _reporteRepository;
    private readonly IPublicadorReporteEventos _eventPublisher;
    private readonly ImagenService _imagenService;

    public EditarReporteHandler(IReporteRepository reporteRepository, IPublicadorReporteEventos eventPublisher, ImagenService imagenService)
    {
        _reporteRepository = reporteRepository;
        _eventPublisher = eventPublisher;
        _imagenService = imagenService;

    }

    public async Task<MessageResponse> Handle(EditarReporteCommand request, CancellationToken cancellationToken)
    {
        var reporte = await _reporteRepository.ObtenerPorIdAsync(request.ReporteId, cancellationToken);
        if (reporte == null)
            return MessageResponse.CrearError("El reporte no existe.");

        // 2. Manejar la imagen (si se proporciona una nueva)
        string rutaImagen = reporte.ImagenRuta;
        if (request.Imagen != null && request.Imagen.Length > 0)
        {
            // Eliminar la imagen anterior si existe
            if (!string.IsNullOrEmpty(rutaImagen))
            {
                _imagenService.EliminarImagen(rutaImagen);
            }
            // Guardar la nueva imagen
            rutaImagen = await _imagenService.GuardarImagen(request.Imagen, request.ReporteId);
        }

        // 3. Actualizar el producto
        reporte.Editar(
            request.Titulo,
            request.Descripcion,
            request.Estado,
            rutaImagen
        );

        // 4. Persistir cambios
        await _reporteRepository.ActualizarAsync(reporte, cancellationToken);

        // 5. Publicar evento de actualización
        await _eventPublisher.PublicarReporteEditadoEvento(new ReporteEditadoEvento
        {
            ReporteId = reporte.IdReporte,
            Titulo = reporte.Titulo,
            Descripcion = reporte.Descripcion,
            ImagenRuta = reporte.ImagenRuta,
        });


        return MessageResponse.CrearExito("Reporte editado exitosamente.");
    }
}
