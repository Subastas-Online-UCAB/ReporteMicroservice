using ReporteService.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioServicio.Infrastructure.Persistencia;
using ReporteService.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ReporteService.Dominio.Eventos;
using ReporteService.Infrastructure.MongoDB.Documents;
using MassTransit;
using ReporteService.Infrastructure.Mongo;
using ReporteService.Application.DTO;
using ReporteService.Infrastructure.MongoDB;

namespace ReporteService.Infrastructure.Repositorios
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IReporteMongoContext _mongoContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReporteRepository(ApplicationDbContext context, IReporteMongoContext mongoContext, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mongoContext = mongoContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> CrearAsync(Reporte reporte, CancellationToken cancellationToken)
        {
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync(cancellationToken);
            return reporte.IdReporte;
        }

        public async Task<Reporte?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Reportes.FirstOrDefaultAsync(s => s.IdReporte == id);
        }

        public async Task ActualizarAsync(Reporte reporte)
        {
            _context.Reportes.Update(reporte);
            await _context.SaveChangesAsync();
        }

        public async Task<Reporte?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Reportes
                .FirstOrDefaultAsync(s => s.IdReporte == id, cancellationToken);
        }

        public async Task ActualizarAsync(Reporte reporte, CancellationToken cancellationToken)
        {
            _context.Reportes.Update(reporte);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CambiarEstadoAsync(Guid idReporte, Guid idUsuario, CancellationToken cancellationToken)
        {
            var reporte = await _context.Reportes.FirstOrDefaultAsync(s => s.IdReporte == idReporte, cancellationToken);
            if (reporte is null)
                throw new Exception("Reporte no encontrado.");


            // PostgreSQL
            reporte.Estado = "Revisado";
            _context.Reportes.Update(reporte);
            await _context.SaveChangesAsync(cancellationToken);

            // MongoDB
            var filter = Builders<ReporteDocument>.Filter.Eq(s => s.Id, idReporte);
            var update = Builders<ReporteDocument>.Update.Set(s => s.Estado, "Revisado");
            await _mongoContext.Reportes.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        }

        public async Task EliminarReporteAsync(Guid idReporte, Guid idUsuario, CancellationToken cancellationToken)
        {
            var producto = await _context.Reportes.FirstOrDefaultAsync(s => s.IdReporte == idReporte, cancellationToken);
            if (producto is null)
                throw new Exception("Reporte no encontrado.");

        }

        public async Task<Reporte?> ObtenerReportePorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var doc = await _mongoContext.Reportes
                .Find(s => s.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doc is null) return null;

            return new Reporte
            {
                IdReporte = doc.Id,
                Titulo = doc.Titulo,
                Descripcion = doc.Descripcion,
                Estado = doc.Estado,
                FechaCreacion = doc.FechaCreacion,
                ImagenRuta = doc.ImagenRuta,
                IdUsuario = doc.IdUsuario,
                IdSubasta = doc.IdSubasta,  
            };
        }


    }
}