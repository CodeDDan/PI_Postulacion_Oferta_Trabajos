using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public class UsuarioIdioma
    {

        [Key]
        public int IDI_ID { get; set; }
        public string UsuarioId { get; set; }
        public string IDI_IDIOMA { get; set; } = null!;
        public string IDI_ESCRITO { get; set; }
        public string IDI_ORAL { get; set; }
    }
    
}
