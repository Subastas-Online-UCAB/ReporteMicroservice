using MassTransit;
using MongoDB.Driver;
using ReporteService.Domain.Eventos;
using ReporteService.Dominio.Eventos;
using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.MongoDB.Documents;

namespace SubastaService.Infraestructura.Consumidor
{
    public class ReporteEditadoConsumidor : IConsumer<ReporteEditadoEvento>
    {
        private readonly IReporteMongoContext _mongoContext;

        public ReporteEditadoConsumidor(IReporteMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Consume(ConsumeContext<ReporteEditadoEvento> context)
        {
            var evento = context.Message;

            var filter = Builders<ReporteDocument>.Filter.Eq(s => s.Id, evento.ReporteId);

            var documentoActual = await _mongoContext.Reportes
                .Find(filter)
                .FirstOrDefaultAsync();


            var updatedDocument = new ReporteDocument
            {
                Id = evento.ReporteId,
                Titulo = evento.Titulo,
                Descripcion = evento.Descripcion,
                ImagenRuta = evento.ImagenRuta,
            };

            await _mongoContext.Reportes.ReplaceOneAsync(
                filter,
                updatedDocument,
                new ReplaceOptions { IsUpsert = true }
            );
        }
    }
}
