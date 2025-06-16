using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using ReporteService.Domain.Events;
using ReporteService.Domain.Interfaces;

namespace ReporteService.Infrastructure.EventPublishers
{
    public class PublicadorReporteEventos : IPublicadorReporteEventos
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublicadorReporteEventos(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublicarReporteCreado(ReporteCreadoEvent evento)
        {
            await _publishEndpoint.Publish(evento);
        }


        public async Task PublicarReporteEditado(ReporteEditadoEvent evento)
        {
            await _publishEndpoint.Publish(evento);
        }

    }
}