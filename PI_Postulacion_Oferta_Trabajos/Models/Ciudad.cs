using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int CidId { get; set; }
        public int ProId { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es requerido.")]
        [StringLength(64, ErrorMessage = "El nombre de la ciudad no puede tener más de 64 caracteres.")]
        [Remote(action: "ValidateUniqueCiudad", controller: "Ciudades", AdditionalFields = "CidId", ErrorMessage = "El nombre de la ciudad ya existe.")]
        public string CidNombre { get; set; } = null!;

        public virtual Provincia Pro { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
