using MediatR;
using ReporteService.Application.Queries;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;

namespace ReporteService.Aplicacion.Handlers
{
    public class GetAllReportesHandler : IRequestHandler<GetAllReportesQuery, List<Reporte>>
    {
        private readonly IMongoReporteRepository _repository;

        public GetAllReportesHandler(IMongoReporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Reporte>> Handle(GetAllReportesQuery request, CancellationToken cancellationToken)
        {
            var subastas = await _repository.ObtenerTodasAsync(cancellationToken);
            return subastas;
        }
    }
}