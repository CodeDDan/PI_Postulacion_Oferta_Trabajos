using System.ComponentModel.DataAnnotations;

namespace PI_Postulacion_Oferta_Trabajos.Models
{
    public class CV
    {

        [Key]
        public int UCV_ID { get; set; }
        public string UsuarioId { get; set; }
        public string UCV_NOMBRE { get; set; } = null!;
    }

}
