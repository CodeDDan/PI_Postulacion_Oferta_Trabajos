using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioEducacion
    {
        public int UseId { get; set; }
        public string UsuarioId { get; set; }
        public string USE_TIPO_FORMACION { get; set; } = null!;
        public string? USE_DOCUMENTO { get; set; }
        public string USE_TITULO { get; set; } = null!;
        public string USE_AREA { get; set; } = null!;
        public string USE_INSTITUCION { get; set; } = null!;
        public string USE_ESTADO { get; set; } = null!;
        public string USE_FECHAI { get; set; } = null!;
        public string USE_FECHAF { get; set; } = null!;

        public virtual Usuario Usu { get; set; } = null!;
    }
}
