using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class EstadoPostulacion
    {
        public EstadoPostulacion()
        {
            Postulaciones = new HashSet<Postulacion>();
        }

        public int EspId { get; set; }
        public string EspNombre { get; set; } = null!;
        public string EspDescripcion { get; set; } = null!;

        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
