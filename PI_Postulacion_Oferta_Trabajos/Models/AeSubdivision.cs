using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class AeSubdivision
    {
        public int AedId { get; set; }
        public int AepId { get; set; }
        public string AedNombre { get; set; } = null!;

        public virtual AeSectorPrincipal Aep { get; set; } = null!;
    }
}
