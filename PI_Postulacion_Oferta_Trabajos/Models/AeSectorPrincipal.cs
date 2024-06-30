using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class AeSectorPrincipal
    {
        public AeSectorPrincipal()
        {
            AeSubdivisions = new HashSet<AeSubdivision>();
            Empresas = new HashSet<Empresa>();
        }

        public int AepId { get; set; }
        public string AepNombre { get; set; } = null!;

        public virtual ICollection<AeSubdivision> AeSubdivisions { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
