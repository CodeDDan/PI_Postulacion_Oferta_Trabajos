using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class PuestoLaboral
    {
        public PuestoLaboral()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int PulId { get; set; }
        public int ArlId { get; set; }
        public string PulNombre { get; set; } = null!;

        public virtual AreaLaboral Arl { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
