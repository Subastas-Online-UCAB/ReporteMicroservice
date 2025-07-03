using MediatR;
using MassTransit;
using ReporteService.Dominio.Repositorios;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Eventos;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Excepciones;

public class CambiarEstadoReporteHandler : IRequestHandler<CambiarEstadoReporteCommand, bool>
{
    private readonly IReporteRepository _repo;
    private readonly IPublishEndpoint _publishEndpoint;

    public CambiarEstadoReporteHandler(IReporteRepository repo, IPublishEndpoint publishEndpoint)
    {
        _repo = repo;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(CambiarEstadoReporteCommand request, CancellationToken cancellationToken)
    {
        var reporte = await _repo.ObtenerPorIdAsync(request.ReporteId, cancellationToken);
        if (reporte == null)
            throw new ReporteNoEncontradoException(request.ReporteId);

        if (reporte.IdUsuario.ToString() != request.IdUsuario)
            throw new UsuarioSinPermisoException(request.IdUsuario);

        // Actualiza el estado en PostgreSQL
        reporte.Estado = request.NuevoEstado;
        await _repo.ActualizarAsync(reporte, cancellationToken);

        // Publica el evento correspondiente a la saga
        switch (request.NuevoEstado)
        {
            case "Active":
                await _publishEndpoint.Publish(new ReporteIniciado
                {
                    ReporteId = reporte.IdReporte.ToString()
                });
                break;

            case "Ended":
                await _publishEndpoint.Publish(new ReporteFinalizado
                {
                    ReporteId = reporte.IdReporte
                });
                break;

        }

        // (Opcional) Publicar también evento de sincronización con Mongo
        await _publishEndpoint.Publish(new ReporteCambioEstado
        {
            ReporteId = reporte.IdReporte,
            NuevoEstado = request.NuevoEstado,
        });

        return true;
    }

    private bool EsTransicionValida(string actual, string nuevo)
    {
        return actual switch
        {
            "Pending" => nuevo == "Active" || nuevo == "Canceled",
            "Active" => nuevo == "Ended" || nuevo == "Canceled",
            _ => false
        };
    }
}
