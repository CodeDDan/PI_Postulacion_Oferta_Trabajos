using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El nombre del sector principal es requerido.")]
        [StringLength(128, ErrorMessage = "El nombre del sector principal no puede tener más de 128 caracteres.")]
        public string AepNombre { get; set; } = null!;

        public virtual ICollection<AeSubdivision> AeSubdivisions { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
