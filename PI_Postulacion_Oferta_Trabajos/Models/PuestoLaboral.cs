using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class PuestoLaboral
    {
        public PuestoLaboral()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int PulId { get; set; }

        [Required(ErrorMessage = "El identificador del área laboral es requerido.")]
        public int ArlId { get; set; }

        // Para que el algoritmo de unicidad funcione con Edit, debe pasarse AdditionalField con el Id correspondiente de la clase
        [Required(ErrorMessage = "El nombre del puesto laboral es requerido.")]
        [StringLength(64, ErrorMessage = "El nombre del puesto laboral no puede superar los 64 caracteres.")]
        [Remote(action: "ValidateUniquePuestoLaboral", controller: "PuestosLaborales", AdditionalFields = "PulId", ErrorMessage = "El nombre del puesto laboral ya existe.")]
        public string PulNombre { get; set; } = null!;

        public virtual AreaLaboral Arl { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
