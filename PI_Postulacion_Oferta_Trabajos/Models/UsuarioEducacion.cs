using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class UsuarioEducacion
    {
        public int UseId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public string UseTipoFormacion { get; set; } = null!;
        public string UseDocumento { get; set; } = null!;
        public string UseDescripcion { get; set; } = null!;

        public virtual Usuario Usu { get; set; } = null!;
    }
}
