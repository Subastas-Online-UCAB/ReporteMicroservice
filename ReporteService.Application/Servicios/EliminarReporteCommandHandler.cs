using MediatR;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;

namespace ReporteService.Application.Handler
{ 
    public class EliminarReporteCommandHandler : IRequestHandler<EliminarReporteCommand, bool>
    {
        private readonly IReporteRepository _repository;

        public EliminarReporteCommandHandler(IReporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(EliminarReporteCommand request, CancellationToken cancellationToken)
        {
            await _repository.EliminarReporteAsync(request.IdReporte, request.IdUsuario, cancellationToken);
            return true;
        }
    }
}
