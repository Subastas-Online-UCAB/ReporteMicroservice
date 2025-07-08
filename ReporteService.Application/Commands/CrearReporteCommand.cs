using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Application.Commands
{
    public class CrearReporteCommand : IRequest<Guid>
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaCreacion { get; set; }
        public IFormFile Imagen { get; set; }
        public Guid IdUsuario { get; set; }

        public Guid IdSubasta { get; set; }
    }
}
