using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioExperienciaLaboral
    {
        public int UsxId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public string UsxEmpresa { get; set; } = null!;
        public string UsxArea { get; set; } = null!;
        public string UsxPuesto { get; set; } = null!;
        public DateTime UsxFechaInicio { get; set; }
        public DateTime? UsxFechaFin { get; set; }
        public string UsxNivelExperiencia { get; set; } = null!;

        public virtual Usuario Usu { get; set; } = null!;
    }
}
