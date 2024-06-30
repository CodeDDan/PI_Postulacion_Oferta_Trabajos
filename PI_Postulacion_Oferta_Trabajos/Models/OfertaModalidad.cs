using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class OfertaModalidad
    {
        public OfertaModalidad()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int OfmId { get; set; }
        public string OfmNombre { get; set; } = null!;

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
