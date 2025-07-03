using ReporteService.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Dominio.Repositorios
{
    public interface IMongoReporteRepository
    {
        Task<List<Reporte>> ObtenerTodasAsync(CancellationToken cancellationToken);
    }
}
