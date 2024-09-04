using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;
using System.Security.Claims;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public TrabajadorController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // Acción para la vista de ofertas con filtros
        public async Task<IActionResult> Index(int? provincia, int? ciudad, int? areaLaboral, int? puestoLaboral,
                                       int? ofertaModalidad, bool? licencia, bool? disponibilidadViajar,
                                       bool? disponibilidadCambioResidencia, bool? discapacidad,
                                       decimal? salarioMinimo, decimal? salarioMaximo, DateTime? fechaInicio,
                                       DateTime? fechaFin, string? tipoContrato)
        {
            // Vamos a obtener el Id de las ofertas a las cuales el usuario está postulado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener ID del usuario actual
            var postuladoOfertas = _context.Postulaciones
                                           .Where(p => p.UsuarioId == userId && p.EspId == 1)
                                           .Select(p => p.OfeId)
                                           .ToList();
            // Cargar la lista de ofertas con los filtros aplicados, aqui se especifican relaciones profundas entre tablas
            var query = _context.Ofertas
                .Include(o => o.Emp)
                .ThenInclude(e => e.Aep)
                .Include(o => o.Pro)
                .Include(o => o.Cid)
                .Include(o => o.Arl)
                .Include(o => o.Pul)
                .Include(o => o.Ofm)
                .AsQueryable();

            // Aplicar los filtros si existen
            if (provincia.HasValue)
                query = query.Where(o => o.ProId == provincia.Value);

            if (ciudad.HasValue)
                query = query.Where(o => o.CidId == ciudad.Value);

            if (areaLaboral.HasValue)
                query = query.Where(o => o.ArlId == areaLaboral.Value);

            if (puestoLaboral.HasValue)
                query = query.Where(o => o.PulId == puestoLaboral.Value);

            if (ofertaModalidad.HasValue)
                query = query.Where(o => o.OfmId == ofertaModalidad.Value);

            if (licencia.HasValue)
                query = query.Where(o => o.OfeLicencia == licencia.Value);

            if (disponibilidadViajar.HasValue)
                query = query.Where(o => o.OfeDisponibilidadViajar == disponibilidadViajar.Value);

            if (disponibilidadCambioResidencia.HasValue)
                query = query.Where(o => o.OfeDisponibilidadCambioResidencia == disponibilidadCambioResidencia.Value);

            if (discapacidad.HasValue)
                query = query.Where(o => o.OfeDiscapacidad == discapacidad.Value);

            if (salarioMinimo.HasValue)
                query = query.Where(o => o.OfeSalario >= salarioMinimo.Value);

            if (salarioMaximo.HasValue)
                query = query.Where(o => o.OfeSalario <= salarioMaximo.Value);

            if (fechaInicio.HasValue)
                query = query.Where(o => o.OfeFechaPublicacion >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(o => o.OfeFechaPublicacion <= fechaFin.Value);

            // Aplicar el filtro de tipo de contrato
            if (!string.IsNullOrEmpty(tipoContrato))
                query = query.Where(o => o.OfeTipoContrato == tipoContrato);

            ViewBag.PostuladoOfertas = postuladoOfertas; // Pasar las ofertas en las que el usuario ya se postuló

            // Ejecutar la consulta y pasar los resultados a la vista
            var ofertas = await query.ToListAsync();

            // Contar el número de postulaciones por oferta
            var postulacionesPorOferta = _context.Postulaciones
                .GroupBy(p => p.OfeId)
                .Select(group => new { OfeId = group.Key, Count = group.Count() })
                .ToList();

            // Convertir la lista a un diccionario para un acceso rápido en la vista
            var postulacionesDict = postulacionesPorOferta.ToDictionary(p => p.OfeId, p => p.Count);

            ViewBag.PostulacionesPorOferta = postulacionesDict;


            // Pasar las listas de filtros a la vista para los dropdowns
            ViewBag.Provincias = await _context.Provincias.ToListAsync();
            ViewBag.Ciudades = await _context.Ciudades.ToListAsync();
            ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
            ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
            ViewBag.OfertasModalidades = await _context.OfertaModalidads.ToListAsync();

            return View(ofertas);
        }

        [HttpPost]
        public async Task<IActionResult> Postularse(int ofeId)
        {
            // Obtener el usuario logeado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Crear una nueva postulación
            var postulacion = new Postulacion
            {
                OfeId = ofeId,
                UsuarioId = userId,
                EspId = 1,  // Estado inicial de postulación
                PosFechaPostulacion = DateTime.Now
            };

            _context.Postulaciones.Add(postulacion);
            await _context.SaveChangesAsync();

            TempData["PostulacionExitosa"] = "¡Postulación Exitosa!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelarPostulacion(int ofeId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postulacion = await _context.Postulaciones
                .FirstOrDefaultAsync(p => p.OfeId == ofeId && p.UsuarioId == usuarioId && p.EspId == 1);

            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
                await _context.SaveChangesAsync();
            }

            // Redirigir al Index con un mensaje de éxito
            TempData["PostulacionCancelada"] = "Postulación cancelada!";
            return RedirectToAction("Index");
        }
    }
}
