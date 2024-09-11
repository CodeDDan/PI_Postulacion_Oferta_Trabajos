using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioDetalle
    {
        public int UsdId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public DateTime? UsdFechaNacimiento { get; set; } // Nullable DateTime
        public string? UsdEstadoCivil { get; set; } // Nullable string
        public string? UsdFoto { get; set; } // Nullable string
        public string? UsdCiudad { get; set; } // Nullable string
        public string? UsdGenero { get; set; } // Nullable string

        public virtual Usuario Usu { get; set; } = null!;
    }
}
