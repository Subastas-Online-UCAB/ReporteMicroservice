using MassTransit;
using ReporteService.Domain.Events;
using ReporteService.Infrastructure.Mongo;
using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.MongoDB.Documents;

namespace ReporteService.Infrastructure.Consumers
{
    public class ReporteCreadoConsumer : IConsumer<ReporteCreadoEvent>
    {
        private readonly IReporteMongoContext _context;

        public ReporteCreadoConsumer(IReporteMongoContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ReporteCreadoEvent> context)
        {
            var mensaje = context.Message;

            var documento = new ReporteDocument
            {
                Id = mensaje.Id,
                Titulo = mensaje.Titulo,
                Descripcion = mensaje.Descripcion,
                Estado = mensaje.Estado,
                IdUsuario = mensaje.IdUsuario,
            };

            await _context.Reportes.InsertOneAsync(documento);
        }
    }
}