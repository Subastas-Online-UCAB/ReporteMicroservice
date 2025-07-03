using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReporteService.Dominio.Entidades;

namespace UsuarioServicio.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Reporte> Reportes { get; set; }

        // Opcionalmente, para ver las tablas creadas, puedes sobreescribir OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Opcional: Cambiar nombre de la tabla si quieres
            modelBuilder.Entity<Reporte>().ToTable("Reportes");

            // Opcional: Configuraciones de columnas si quieres afinar
            modelBuilder.Entity<Reporte>(entity =>
            {

                entity.HasKey(s => s.IdReporte);

                entity.Property(s => s.Titulo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(s => s.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.FechaCreacion)
                    .IsRequired();

                entity.Property(s => s.IdUsuario)
                    .IsRequired();

                entity.Property(s => s.IdSubasta)
                    .IsRequired();

            });
        }
    }
}

