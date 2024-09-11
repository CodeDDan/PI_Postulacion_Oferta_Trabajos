using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class AreaLaboral
    {
        public AreaLaboral()
        {
            Oferta = new HashSet<Oferta>();
            PuestosLaborales = new HashSet<PuestoLaboral>();
        }

        public int ArlId { get; set; }

        // Para que el algoritmo de unicidad funcione con Edit, debe pasarse AdditionalField con el Id correspondiente de la clase
        [Required(ErrorMessage = "El nombre del área laboral es requerido.")]
        [StringLength(64, ErrorMessage = "El nombre del área laboral no puede tener más de 64 caracteres.")]
        [Remote(action: "ValidateUniqueAreaLaboral", controller: "AreasLaborales", AdditionalFields = "ArlId", ErrorMessage = "El nombre del área laboral ya existe.")]
        public string ArlNombre { get; set; } = null!;

        public virtual ICollection<Oferta> Oferta { get; set; }
        public virtual ICollection<PuestoLaboral> PuestosLaborales { get; set; }
    }
}
