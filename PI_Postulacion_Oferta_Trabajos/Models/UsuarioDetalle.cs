using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioDetalle
    {
        public int UsdId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public DateTime UsdFechaNacimiento { get; set; }
        public string UsdEstadoCivil { get; set; } = null!;
        public string? UsdFoto { get; set; }
        public string? UsdCiudad { get; set; }
        public string UsdGenero { get; set; } = null!;

        public virtual Usuario Usu { get; set; } = null!;
    }
}
