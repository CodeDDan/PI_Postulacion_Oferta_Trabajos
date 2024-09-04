using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class EmpleadorController : Controller
    {
        private readonly PO_TrabajosContext _context;
        private readonly UserManager<Usuario> _userManager;

        public EmpleadorController(PO_TrabajosContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Formulario para publicar oferta
        public async Task<IActionResult> PublicarOferta()
        {
            // Cargar los datos necesarios para los dropdowns
            ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
            ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
            ViewBag.OfertaModalidades = await _context.OfertaModalidads.ToListAsync();
            ViewBag.Provincias = await _context.Provincias.ToListAsync();
            ViewBag.Ciudades = await _context.Ciudades.ToListAsync();

            return View();
        }

        // POST: Crear oferta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublicarOferta(Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.GetUserAsync(User);

                if (usuario != null && usuario.UsuarioIdEmpresa.HasValue)
                {
                    oferta.EmpId = usuario.UsuarioIdEmpresa.Value;
                    oferta.OfeFechaPublicacion = DateTime.Now;

                    // Validar existencia de AreaLaboral y PuestoLaboral
                    var areaLaboral = await _context.AreasLaborales.FindAsync(oferta.ArlId);
                    var puestoLaboral = await _context.PuestosLaborales.FindAsync(oferta.PulId);

                    if (areaLaboral == null)
                    {
                        ModelState.AddModelError("", "Área Laboral no válida.");
                    }

                    if (puestoLaboral == null)
                    {
                        ModelState.AddModelError("", "Puesto Laboral no válido.");
                    }

                    if (ModelState.IsValid)
                    {
                        _context.Ofertas.Add(oferta);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Home"); // Redirigir a la vista principal del empleador
                    }
                }
                ModelState.AddModelError("", "No se pudo identificar el usuario empleador.");
            }

            // Cargar los datos necesarios para los dropdowns en caso de error
            ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
            ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
            ViewBag.OfertaModalidades = await _context.OfertaModalidads.ToListAsync();
            ViewBag.Provincias = await _context.Provincias.ToListAsync();
            ViewBag.Ciudades = await _context.Ciudades.ToListAsync();

            return View(oferta);
        }

        public async Task<IActionResult> VerPostulaciones(DateTime? fechaDesde, DateTime? fechaHasta, int? estadoPostulacion, bool? tienePostulantes)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null && usuario.UsuarioIdEmpresa.HasValue)
            {
                var empresaId = usuario.UsuarioIdEmpresa.Value;

                // Obtener las ofertas publicadas por la empresa
                var ofertas = _context.Ofertas
                    .Include(o => o.Postulaciones)
                    .Where(o => o.EmpId == empresaId)
                    .AsQueryable();

                // Filtros
                if (fechaDesde.HasValue && fechaHasta.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfeFechaPublicacion.Date >= fechaDesde.Value.Date
                                                 && o.OfeFechaPublicacion.Date <= fechaHasta.Value.Date);
                }
                else if (fechaDesde.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfeFechaPublicacion.Date >= fechaDesde.Value.Date);
                }
                else if (fechaHasta.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfeFechaPublicacion.Date <= fechaHasta.Value.Date);
                }

                if (estadoPostulacion.HasValue)
                {
                    ofertas = ofertas.Where(o => o.Postulaciones.Any(p => p.EspId == estadoPostulacion.Value));
                }

                if (tienePostulantes.HasValue)
                {
                    if (tienePostulantes.Value)
                    {
                        ofertas = ofertas.Where(o => o.Postulaciones.Any());
                    }
                    else
                    {
                        ofertas = ofertas.Where(o => !o.Postulaciones.Any());
                    }
                }

                var modelo = await ofertas.ToListAsync();

                ViewBag.EstadosPostulacion = await _context.EstadoPostulacions.ToListAsync();

                return View(modelo);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
