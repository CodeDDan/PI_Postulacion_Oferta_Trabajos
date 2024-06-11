using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int EmpId { get; set; }
        public string EmpNombre { get; set; } = null!;
        public string EmpRuc { get; set; } = null!;
        public string EmpRazonSocial { get; set; } = null!;
        public string EmpDireccion { get; set; } = null!;
        public string EmpCorreo { get; set; } = null!;
        public decimal EmpTelefono { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
