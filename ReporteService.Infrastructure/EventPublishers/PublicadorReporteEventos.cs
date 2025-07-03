using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using ReporteService.Dominio.Eventos;
using ReporteService.Dominio.Interfaces;

namespace ReporteService.Infrastructure.EventPublishers
{
    public class PublicadorReporteEventos : IPublicadorReporteEventos
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublicadorReporteEventos(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublicarReporteCreado(ReporteCreadoEvento evento)
        {
            await _publishEndpoint.Publish(evento);
        }

    }
}