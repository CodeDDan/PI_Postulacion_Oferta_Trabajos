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
        [StringLength(64, ErrorMessage = "El nombre del tipo de oferta no puede tener más de 64 caracteres.")]
        [Remote(action: "ValidateUniqueOfertaTipo", controller: "OfertaModalidades", AdditionalFields = "OfmId", ErrorMessage = "El tipo de modalidad ya existe.")]
        public string OfmNombre { get; set; } = null!;

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
