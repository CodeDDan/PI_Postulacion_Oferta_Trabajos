using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Postulacion
    {
        public int PosId { get; set; }

        [Required(ErrorMessage = "El campo Oferta es requerido.")]
        public int OfeId { get; set; }

        [Required(ErrorMessage = "El campo Usuario es requerido.")]
        public string UsuarioId { get; set; } = null!;

        [Required(ErrorMessage = "El campo Estado de Postulación es requerido.")]
        public int EspId { get; set; }

        [Required(ErrorMessage = "La fecha de postulación es requerida.")]
        public DateTime PosFechaPostulacion { get; set; }
        public virtual EstadoPostulacion Esp { get; set; } = null!;
        public virtual Oferta Ofe { get; set; } = null!;
        public virtual Usuario Usu { get; set; } = null!;
    }
}
