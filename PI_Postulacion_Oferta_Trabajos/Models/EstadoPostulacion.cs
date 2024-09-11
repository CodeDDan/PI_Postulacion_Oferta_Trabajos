using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class EstadoPostulacion
    {
        public EstadoPostulacion()
        {
            Postulaciones = new HashSet<Postulacion>();
        }

        public int EspId { get; set; }

        [Required(ErrorMessage = "El nombre del estado de postulación es obligatorio.")]
        [StringLength(64, ErrorMessage = "El nombre del estado de postulación no puede tener más de 64 caracteres.")]
        public string EspNombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción del estado de postulación es obligatoria.")]
        [StringLength(128, ErrorMessage = "La descripción del estado de postulación no puede tener más de 128 caracteres.")]
        public string EspDescripcion { get; set; } = null!;

        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
