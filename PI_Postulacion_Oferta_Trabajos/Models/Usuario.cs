using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Postulaciones = new HashSet<Postulacion>();
        }

        public int UsuId { get; set; }
        public int TipId { get; set; }
        public string UsuNombre { get; set; } = null!;
        public string UsuApellido { get; set; } = null!;
        public string UsuCorreo { get; set; } = null!;
        public string UsuPassword { get; set; } = null!;
        public string UsuDireccion { get; set; } = null!;
        public decimal UsuTelefono { get; set; }
        public string UsuCedula { get; set; } = null!;
        public DateTime UsuFechaNacimiento { get; set; }
        public string UsuCurriculumVitae { get; set; } = null!;

        public virtual TipoUsuario Tip { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
    }
}
