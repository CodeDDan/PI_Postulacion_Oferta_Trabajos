using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioPerfil
    {
        public int UspId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public string UspAspiracionLarboral { get; set; } = null!;
        public decimal UspPreferenciaSalarial { get; set; }
        public string? UspHojaVida { get; set; }
        public string? UspDiscapacidad { get; set; }

        public virtual Usuario Usu { get; set; } = null!;
    }
}
