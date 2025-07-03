using MassTransit;
using ReporteService.Dominio.Eventos;
using ReporteService.Infrastructure.Mongo;
using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.MongoDB.Documents;

namespace ReporteService.Infrastructure.Consumers
{
    public class ReporteCreadoConsumer : IConsumer<ReporteCreadoEvento>
    {
        private readonly IReporteMongoContext _context;

        public ReporteCreadoConsumer(IReporteMongoContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ReporteCreadoEvento> context)
        {
            var mensaje = context.Message;

            var documento = new ReporteDocument
            {
                Id = mensaje.Id,
                Titulo = mensaje.Titulo,
                Descripcion = mensaje.Descripcion,
                Estado = mensaje.Estado,
                FechaCreacion = mensaje.FechaCreacion,
                IdUsuario = mensaje.IdUsuario,
                IdSubasta = mensaje.IdSubasta, 
            };

            await _context.Reportes.InsertOneAsync(documento);
        }
    }
}