using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Oferta
    {
        public Oferta()
        {
            Postulaciones = new HashSet<Postulacion>();
        }

        public int OfeId { get; set; }
        public int EmpId { get; set; }
        public int OfmId { get; set; }
        public int PulId { get; set; }
        public int CidId { get; set; }
        public int ArlId { get; set; }
        public string OfeTitulo { get; set; } = null!;
        public string OfeDescripcion { get; set; } = null!;
        public string OfeTipoContrato { get; set; } = null!;
        public decimal OfeSalario { get; set; }
        public DateTime OfeFechaPublicacion { get; set; }
        public decimal OfeCantidadVacantes { get; set; }
        public int OfeTiempoExperiencia { get; set; }
        public string OfeEducacionMinima { get; set; } = null!;
        public bool OfeLicencia { get; set; }
        public bool OfeDisponibilidadViajar { get; set; }
        public bool OfeDisponibilidadCambioResidencia { get; set; }
        public bool OfeDiscapacidad { get; set; }
        public int? OfeEdadMinima { get; set; }
        public string? OfeAreaLaboral { get; set; }

        public virtual AreaLaboral Arl { get; set; } = null!;
        public virtual Ciudad Cid { get; set; } = null!;
        public virtual Empresa Emp { get; set; } = null!;
        public virtual OfertaModalidad Ofm { get; set; } = null!;
        public virtual PuestoLaboral Pul { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
