using PI_Postulacion_Oferta_Trabajos.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "El campo Nombre debe tener entre 3 y 64 caracteres.")]
        public string UsuNombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "El campo Apellido debe tener entre 3 y 64 caracteres.")]
        public string UsuApellido { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El campo Correo no es una dirección de correo electrónico válida.")]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "El campo Correo debe tener entre 3 y 128 caracteres.")]
        public string UsuCorreo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string UsuPassword { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        //[Phone(ErrorMessage = "El número de teléfono no es válido.")] No valida numeros ecuatorianos
        public decimal UsuTelefono { get; set; }

        [Required(ErrorMessage = "La cédula es requerida.")]
        [CedulaEcuatoriana(ErrorMessage = "Cédula ecuatoriana no válida.")]
        public string UsuCedula { get; set; } = null!;

        public virtual TipoUsuario Tip { get; set; } = null!;
        public virtual ICollection<Postulacion> Postulaciones { get; set; }
        public virtual ICollection<UsuarioDetalle> UsuarioDetalles { get; set; }
        public virtual ICollection<UsuarioEducacion> UsuarioEducacions { get; set; }
        public virtual ICollection<UsuarioExperienciaLaboral> UsuarioExperienciaLaborals { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
    }
}
