using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models.IdentityModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The role name must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }
    }
}
