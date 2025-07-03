using MongoDB.Driver;
using ReporteService.Dominio.Entidades;
using ReporteService.Dominio.Repositorios;

using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.MongoDB.Documents;

namespace ReporteService.Infraestructura.Repositorios
{
    public class MongoAuctionRepository : IMongoReporteRepository
    {
        private readonly IMongoCollection<ReporteDocument> _collection;

        public MongoAuctionRepository(IReporteMongoContext context)
        {
            _collection = context.Reportes;
        }

         public async Task<List<Reporte>> ObtenerTodasAsync(CancellationToken cancellationToken)
        {
            var documentos = await _collection.Find(_ => true).ToListAsync(cancellationToken);

            return documentos.Select(doc => new Reporte
            {
                IdReporte = doc.Id,
                Titulo = doc.Titulo,
                Descripcion = doc.Descripcion,
                Estado = doc.Estado,
                FechaCreacion = doc.FechaCreacion,
                IdUsuario = doc.IdUsuario,
                IdSubasta = doc.IdSubasta,
            }).ToList();

        }
    }
}