using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Oferta = new HashSet<Oferta>();
        }

        
        public int EmpId { get; set; }
        [Required(ErrorMessage = "El campo AepId es obligatorio.")]
        public int AepId { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(64, ErrorMessage = "El nombre de la empresa no puede tener más de 64 caracteres.")]
        public string EmpNombreEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico de registro es obligatorio.")]
        [StringLength(128, ErrorMessage = "El correo electrónico de registro no puede tener más de 128 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico de registro no es válido.")]
        public string EmpEmailRegistro { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico de acceso es obligatorio.")]
        [StringLength(128, ErrorMessage = "El correo electrónico de acceso no puede tener más de 128 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico de acceso no es válido.")]
        public string EmpEmailAcceso { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(128, ErrorMessage = "La contraseña no puede tener más de 128 caracteres.")]
        public string EmpPassword { get; set; } = null!;

        [Required(ErrorMessage = "El RUC es obligatorio.")]
        [StringLength(20, ErrorMessage = "El RUC no puede tener más de 20 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El RUC solo puede contener dígitos.")]
        public string EmpRuc { get; set; } = null!;

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        [StringLength(128, ErrorMessage = "La razón social no puede tener más de 128 caracteres.")]
        public string EmpRazonSocial { get; set; } = null!;

        [StringLength(128, ErrorMessage = "La ciudad no puede tener más de 128 caracteres.")]
        public string? EmpCiudad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El teléfono debe ser un número positivo.")]
        public decimal EmpTelefono { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El número de trabajadores debe ser un número positivo.")]
        public decimal? EmpNumeroTrabajadores { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El número de vacantes anuales debe ser un número positivo.")]
        public decimal? EmpVacantesAnuales { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede tener más de 1000 caracteres.")]
        public string? EmpDescripcion { get; set; }

        public virtual AeSectorPrincipal Aep { get; set; } = null!;
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
