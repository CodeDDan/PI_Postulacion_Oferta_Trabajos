using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Administrador
    {
        public int AdmId { get; set; }
        public string AdmNombre { get; set; } = null!;
        public string AdmApellido { get; set; } = null!;
        public string AdmCorreo { get; set; } = null!;
        public string AdmPassword { get; set; } = null!;
        public string? AdmDireccion { get; set; }
    }
}
