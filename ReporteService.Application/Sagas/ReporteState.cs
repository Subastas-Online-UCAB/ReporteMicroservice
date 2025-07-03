using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace ReporteService.Application.Sagas
{
    public class ReporteState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }  // antes Guid
        public string CurrentState { get; set; } = null!;
        public string ReporteId { get; set; } = null!;

        // 👉 requerido por MongoDb saga storage
        public int Version { get; set; }
    }
}
