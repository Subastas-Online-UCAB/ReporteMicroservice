using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Application.DTO
{
    public class ReporteCompletoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdSubasta { get; set; }
    }
}
