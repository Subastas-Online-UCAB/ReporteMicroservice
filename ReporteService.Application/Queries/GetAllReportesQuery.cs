using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporteService.Application.DTO;
using ReporteService.Dominio.Entidades;

namespace ReporteService.Application.Queries
{
    public class GetAllReportesQuery : IRequest<List<Reporte>> { }

}
