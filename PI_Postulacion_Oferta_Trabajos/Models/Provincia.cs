using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Provincia
    {
        public Provincia()
        {
            Ciudades = new HashSet<Ciudad>();
        }

        public int ProId { get; set; }
        public string ProNombre { get; set; } = null!;

        public virtual ICollection<Ciudad> Ciudades { get; set; }
        public virtual ICollection<Oferta> Ofertas { get; set; }
    }
}
