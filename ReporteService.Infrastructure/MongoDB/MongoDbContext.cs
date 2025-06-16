using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.MongoDB.Documents;

namespace ReporteService.Infrastructure.Mongo
{
    public class MongoDbContext : IReporteMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<ReporteDocument> Reportes =>
            _database.GetCollection<ReporteDocument>("reportes");

        public IMongoDatabase Database => _database;
    }
}
