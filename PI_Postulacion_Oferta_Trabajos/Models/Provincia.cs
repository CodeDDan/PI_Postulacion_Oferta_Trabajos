using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Provincia
    {
        public Provincia()
        {
            Ciudades = new HashSet<Ciudad>();
        }

        public int ProId { get; set; }

        [Required(ErrorMessage = "El nombre de la provincia es requerido.")]
        [StringLength(64, ErrorMessage = "El nombre de la provincia no puede superar los 64 caracteres.")]
        public string ProNombre { get; set; } = null!;

        public virtual ICollection<Ciudad> Ciudades { get; set; }
        public virtual ICollection<Oferta> Ofertas { get; set; }
    }
}
