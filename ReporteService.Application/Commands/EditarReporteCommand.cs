using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ReporteService.Application.Comun;

namespace ReporteService.Application.Commands
{
    public record EditarReporteCommand(
        Guid ReporteId,
        Guid UsuarioId,
        string Titulo,
        string Descripcion,
        string Estado
    ) : IRequest<MessageResponse>;
}
