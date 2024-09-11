using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Oferta
    {
        public Oferta()
        {
            Postulaciones = new HashSet<Postulacion>();
        }

        public int OfeId { get; set; }

        [Required(ErrorMessage = "El identificador de la empresa es requerido.")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "El identificador de la modalidad es requerido.")]
        public int OfmId { get; set; }

        [Required(ErrorMessage = "El identificador del puesto es requerido.")]
        public int PulId { get; set; }

        [Required(ErrorMessage = "El identificador de la ciudad es requerido.")]
        public int CidId { get; set; }

        [Required(ErrorMessage = "El identificador del área laboral es requerido.")]
        public int ArlId { get; set; }

        [Required(ErrorMessage = "El identificador de la provincia es requerido.")]
        public int ProId { get; set; }  // Nueva propiedad

        [Required(ErrorMessage = "El título de la oferta es requerido.")]
        [StringLength(64, ErrorMessage = "El título de la oferta no puede tener más de 64 caracteres.")]
        public string OfeTitulo { get; set; } = null!;

        [Required(ErrorMessage = "La descripción de la oferta es requerida.")]
        [StringLength(128, ErrorMessage = "La descripción de la oferta no puede tener más de 128 caracteres.")]
        public string OfeDescripcion { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de contrato es requerido.")]
        [StringLength(64, ErrorMessage = "El tipo de contrato no puede tener más de 64 caracteres.")]
        public string OfeTipoContrato { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal OfeSalario { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es requerida.")]
        [DataType(DataType.Date)]
        public DateTime OfeFechaPublicacion { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La cantidad de vacantes debe ser un valor positivo.")]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal OfeCantidadVacantes { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El tiempo de experiencia debe ser un valor positivo.")]
        public int OfeTiempoExperiencia { get; set; }

        [Required(ErrorMessage = "La educación mínima es requerida.")]
        [StringLength(64, ErrorMessage = "La educación mínima no puede tener más de 64 caracteres.")]
        public string OfeEducacionMinima { get; set; } = null!;
        public bool OfeLicencia { get; set; }
        public bool OfeDisponibilidadViajar { get; set; }
        public bool OfeDisponibilidadCambioResidencia { get; set; }
        public bool OfeDiscapacidad { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La edad mínima debe ser un valor positivo.")]
        public int? OfeEdadMinima { get; set; }
        public virtual AreaLaboral Arl { get; set; } = null!;
        public virtual Ciudad Cid { get; set; } = null!;
        public virtual Empresa Emp { get; set; } = null!;
        public virtual OfertaModalidad Ofm { get; set; } = null!;
        public virtual PuestoLaboral Pul { get; set; } = null!;
        public virtual Provincia Pro { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
