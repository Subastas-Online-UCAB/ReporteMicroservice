using MongoDB.Driver;
using ReporteService.Infrastructure.MongoDB.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Infrastructure.MongoDB
{
    public interface IReporteMongoContext
    {
        IMongoCollection<ReporteDocument> Reportes { get; }
    }
}
