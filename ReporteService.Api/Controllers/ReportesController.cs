using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReporteService.Application.Commands;
using ReporteService.Application.Queries;
using ReporteService.Application.Request;
using ReporteService.Dominio.Entidades;

namespace ReporteService.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones relacionadas con reportes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo reporte.
        /// </summary>
        /// <param name="command">Datos del reporte a crear.</param>
        /// <returns>ID del reporte creado.</returns>
        /// <response code="201">Reporte creado exitosamente.</response>
        /// <response code="400">Datos inválidos o incompletos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearReporte([FromForm] CrearReporteCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        /// <summary>
        /// Obtiene un reporte por su ID (placeholder temporal).
        /// </summary>
        /// <param name="id">ID del reporte.</param>
        /// <returns>Objeto con el ID y mensaje de confirmación.</returns>
        /// <response code="200">Reporte encontrado (placeholder).</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            // Este es un placeholder hasta que hagamos un query real
            return Ok(new { Id = id, Mensaje = "Reporte recuperado (placeholder)" });
        }

        /// <summary>
        /// Consulta un reporte por su ID.
        /// </summary>
        /// <param name="id">ID del reporte a consultar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Detalles del reporte si existe.</returns>
        /// <response code="200">Reporte encontrado.</response>
        /// <response code="404">Reporte no encontrado.</response>
        [HttpGet("buscar/{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(new ConsultarReportePorIdQuery(id), cancellationToken);

            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }


        /// <summary>
        /// Obtiene la lista de todos los reportes.
        /// </summary>
        /// <remarks>
        /// Retorna reportes registradas desde la base de datos de lectura (MongoDB).
        /// No requiere parámetros de entrada.
        /// </remarks>
        /// <returns>Lista de objetos <see cref="Reporte"/>.</returns>
        /// <response code="200">Lista de Reportes obtenida exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await _mediator.Send(new GetAllReportesQuery());
            return Ok(resultado);
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoRequest request)
        {
            var command = new CambiarEstadoReporteCommand
            {
                ReporteId = id,
                NuevoEstado = request.NuevoEstado,
                IdUsuario = request.IdUsuario
            };

            var result = await _mediator.Send(command);
            return result ? Ok("Estado actualizado correctamente.") : BadRequest("No se pudo actualizar el estado.");
        }

        [HttpPut("editar")]
        public async Task<IActionResult> EditarReporte(Guid id, [FromForm] EditarReporteCommand command)
        {
            command.ReporteId = id; // Asignar el ID desde la ruta
            var resultado = await _mediator.Send(command);
            return resultado.Success ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid usuarioId)
        {
            var resultado = await _mediator.Send(new EliminarReporteCommand()
            {
                IdReporte = id,
                IdUsuario = usuarioId
            });

            if (!resultado)
                return NotFound();

            return NoContent();
        }



    }
}

