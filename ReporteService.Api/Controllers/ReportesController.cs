using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReporteService.Application.Commands;
using ReporteService.Application.Queries;

namespace ReporteService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReporte([FromBody] CrearReporteCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // Este es un placeholder hasta que hagamos un query real
            return Ok(new { Id = id, Mensaje = "Reporte recuperado (placeholder)" });
        }


        [HttpPut("editar")]
        public async Task<IActionResult> EditarReporte([FromBody] EditarReporteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet("buscar/{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(new ConsultarReportePorIdQuery(id), cancellationToken);

            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }


    }
}
