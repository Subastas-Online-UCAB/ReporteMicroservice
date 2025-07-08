using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ReporteService.Application.Comun;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace ReporteService.Application.Commands
{
    public class EditarReporteCommand : IRequest<MessageResponse>
    {
        public Guid ReporteId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public IFormFile Imagen { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid SubastaId { get; set; }

    }
}
