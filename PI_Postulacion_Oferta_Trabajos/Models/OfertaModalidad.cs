using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class OfertaModalidad
    {
        public OfertaModalidad()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int OfmId { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de oferta es requerido.")]
        [Remote(action: "ValidateUniqueOfertaTipo", controller: "OfertaModalidades", AdditionalFields = "OfmId", ErrorMessage = "El tipo de modalidad ya existe.")]
        public string OfmNombre { get; set; } = null!;

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
