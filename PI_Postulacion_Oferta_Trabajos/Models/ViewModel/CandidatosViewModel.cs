namespace PI_Postulacion_Oferta_Trabajos.Models.ViewModel
{
    public class CandidatosViewModel
    {

        public int OfertaId { get; set; } 
        public string NombreOferta { get; set; }
        public string ProvinciaOferta { get; set; }
        public string CiudadOferta { get; set; }
        public IEnumerable<Postulacion> Postulaciones { get; set; }

        public CandidatosViewModel(int ofertaId, string nombreOferta, string provinciaOferta, string ciudadOferta, IEnumerable<Postulacion> postulaciones)
        {
            OfertaId = ofertaId;
            NombreOferta = nombreOferta ?? throw new ArgumentNullException(nameof(nombreOferta));
            ProvinciaOferta = provinciaOferta ?? throw new ArgumentNullException(nameof(provinciaOferta));
            CiudadOferta = ciudadOferta ?? throw new ArgumentNullException(nameof(ciudadOferta));
            Postulaciones = postulaciones ?? throw new ArgumentNullException(nameof(postulaciones));
        }
    }
}
