using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int CidId { get; set; }
        public int ProId { get; set; }
        public string CidNombre { get; set; } = null!;

        public virtual Provincia Pro { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
