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
        public string OfeTitulo { get; set; } = null!;
        public string OfeDescripcion { get; set; } = null!;
        public string OfeRequisitos { get; set; } = null!;
        public decimal OfeSalario { get; set; }
        public DateTime OfeFechaPublicacion { get; set; }

        public virtual Empresa Emp { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
