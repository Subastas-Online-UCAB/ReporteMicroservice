using MediatR;
using ReporteService.Application.DTO;
using ReporteService.Dominio.Entidades;


namespace ReporteService.Application.Queries
{
    public class ConsultarReportePorIdQuery : IRequest<ReporteCompletoDto?>
    {
        public Guid IdReporte { get; }

        public ConsultarReportePorIdQuery(Guid idReporte)
        {
            IdReporte = idReporte;
        }
    }
}