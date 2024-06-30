using System;
using System.Collections.Generic;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Postulaciones = new HashSet<Postulacion>();
            UsuarioDetalles = new HashSet<UsuarioDetalle>();
            UsuarioEducacions = new HashSet<UsuarioEducacion>();
            UsuarioExperienciaLaborals = new HashSet<UsuarioExperienciaLaboral>();
            UsuarioPerfils = new HashSet<UsuarioPerfil>();
        }

        public int UsuId { get; set; }
        public int TipId { get; set; }
        public string UsuNombre { get; set; } = null!;
        public string UsuApellido { get; set; } = null!;
        public string UsuCorreo { get; set; } = null!;
        public string UsuPassword { get; set; } = null!;
        public decimal UsuTelefono { get; set; }
        public string UsuCedula { get; set; } = null!;

        public virtual TipoUsuario Tip { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
        public virtual ICollection<UsuarioDetalle> UsuarioDetalles { get; set; }
        public virtual ICollection<UsuarioEducacion> UsuarioEducacions { get; set; }
        public virtual ICollection<UsuarioExperienciaLaboral> UsuarioExperienciaLaborals { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
    }
}
