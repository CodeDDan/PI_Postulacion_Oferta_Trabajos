using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class AreaLaboral
    {
        public AreaLaboral()
        {
            Oferta = new HashSet<Oferta>();
            PuestosLaborales = new HashSet<PuestoLaboral>();
        }

        public int ArlId { get; set; }
        public string ArlNombre { get; set; } = null!;

        public virtual ICollection<Oferta> Oferta { get; set; }
        public virtual ICollection<PuestoLaboral> PuestosLaborales { get; set; }
    }
}
