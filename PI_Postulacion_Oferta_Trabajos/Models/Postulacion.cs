using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Postulacion
    {
        public int PosId { get; set; }
        public int OfeId { get; set; }
        public int UsuId { get; set; }
        public int EspId { get; set; }
        public DateTime PosFechaPostulacion { get; set; }

        public virtual EstadoPostulacion Esp { get; set; } = null!;
        public virtual Oferta Ofe { get; set; } = null!;
        public virtual Usuario Usu { get; set; } = null!;
    }
}
