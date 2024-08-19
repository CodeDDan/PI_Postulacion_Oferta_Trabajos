using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class AeSubdivision
    {
        public int AedId { get; set; }

        [Required]
        public int AepId { get; set; }

        [Required]
        [StringLength(100)]
        public string AedNombre { get; set; } = null!;

        public virtual AeSectorPrincipal Aep { get; set; } = null!;
    }
}
