﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteService.Application.Request
{
    public class CambiarEstadoRequest
    {
        public string NuevoEstado { get; set; } = string.Empty;
        public string IdUsuario { get; set; } = string.Empty;

        public string IdSubasta { get; set; } = string.Empty;

    }
}
