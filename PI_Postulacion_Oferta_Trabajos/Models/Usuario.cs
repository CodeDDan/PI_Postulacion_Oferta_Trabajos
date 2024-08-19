using Microsoft.AspNetCore.Identity;
using PI_Postulacion_Oferta_Trabajos.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Usuario : IdentityUser
    {
        public Usuario()
        {
            Postulaciones = new HashSet<Postulacion>();
            UsuarioDetalles = new HashSet<UsuarioDetalle>();
            UsuarioEducacions = new HashSet<UsuarioEducacion>();
            UsuarioExperienciaLaborals = new HashSet<UsuarioExperienciaLaboral>();
            UsuarioPerfils = new HashSet<UsuarioPerfil>();
        }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "El campo Nombre debe tener entre 3 y 64 caracteres.")]
        public string UsuNombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "El campo Apellido debe tener entre 3 y 64 caracteres.")]
        public string UsuApellido { get; set; } = null!;

        [Required(ErrorMessage = "La cédula es requerida.")]
        [CedulaEcuatoriana(ErrorMessage = "Cédula ecuatoriana no válida.")]
        public string UsuCedula { get; set; } = null!;
        public int? UsuarioIdEmpresa { get; set; }

        public virtual ICollection<Postulacion> Postulaciones { get; set; }
        public virtual ICollection<UsuarioDetalle> UsuarioDetalles { get; set; }
        public virtual ICollection<UsuarioEducacion> UsuarioEducacions { get; set; }
        public virtual ICollection<UsuarioExperienciaLaboral> UsuarioExperienciaLaborals { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
    }
}
