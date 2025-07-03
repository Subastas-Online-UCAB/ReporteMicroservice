using MassTransit;
using MongoDB.Driver;
using ReporteService.Dominio.Eventos;
using ReporteService.Infrastructure.Mongo;
using ReporteService.Infrastructure.MongoDB.Documents;

public class ReporteStateChangedConsumer : IConsumer<ReporteCambioEstado>
{
    private readonly MongoDbContext _mongo;

    public ReporteStateChangedConsumer(MongoDbContext mongo)
    {
        _mongo = mongo;
    }

    public async Task Consume(ConsumeContext<ReporteCambioEstado> context)
    {
        var mensaje = context.Message;

        var filter = Builders<ReporteDocument>.Filter.Eq(a => a.Id, mensaje.ReporteId);
        var update = Builders<ReporteDocument>.Update
            .Set(a => a.Estado, mensaje.NuevoEstado);

        await _mongo.Reportes.UpdateOneAsync(filter, update);
    }
}