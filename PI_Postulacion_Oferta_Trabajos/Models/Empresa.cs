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
        public int AepId { get; set; }
        public string EmpNombreEmpresa { get; set; } = null!;
        public string EmpEmailRegistro { get; set; } = null!;
        public string EmpEmailAcceso { get; set; } = null!;
        public string EmpPassword { get; set; } = null!;
        public string EmpRuc { get; set; } = null!;
        public string EmpRazonSocial { get; set; } = null!;
        public string? EmpCiudad { get; set; }
        public decimal EmpTelefono { get; set; }
        public decimal? EmpNumeroTrabajadores { get; set; }
        public decimal? EmpVacantesAnuales { get; set; }
        public string? EmpDescripcion { get; set; }

        public virtual AeSectorPrincipal Aep { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
