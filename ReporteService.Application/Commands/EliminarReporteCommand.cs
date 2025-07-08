using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Application.Commands
{
    public class EliminarReporteCommand : IRequest<bool>
    {
        public Guid IdReporte { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
