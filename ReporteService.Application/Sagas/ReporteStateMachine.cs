using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using ReporteService.Dominio.Eventos;

namespace ReporteService.Application.Sagas
{
    public class ReporteStateMachine : MassTransitStateMachine<ReporteState>
    {
        public State Pending { get; private set; } = null!;
        public State Active { get; private set; } = null!;
        public State Ended { get; private set; } = null!;

        public Event<ReporteIniciado> ReporteIniciado { get; private set; } = null!;
        public Event<ReporteFinalizado> ReporteFinalizado { get; private set; } = null!;

        public ReporteStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => ReporteIniciado, x => x.CorrelateById(ctx => Guid.Parse(ctx.Message.ReporteId)));
            Event(() => ReporteFinalizado, x => x.CorrelateById(ctx => ctx.Message.ReporteId));

            Initially(
                When(ReporteIniciado)
                    .Then(ctx =>
                    {
                        Console.WriteLine($"Saga activada para ReporteId: {ctx.Message.ReporteId}");
                        ctx.Saga.CorrelationId = Guid.Parse(ctx.Message.ReporteId);
                        ctx.Saga.ReporteId = ctx.Message.ReporteId;
                    })
                    .TransitionTo(Pending)
            );

            During(Active,
                When(ReporteFinalizado)
                    .TransitionTo(Ended)
            );


            During(Pending,

                When(ReporteIniciado)
                    .Then(ctx => Console.WriteLine($"Subasta activada"))
                    .TransitionTo(Active)
            );
        }
    }
}