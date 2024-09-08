using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Models.ViewModel;
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

        //GET: Datos de la empresa
        [HttpGet]

        public async Task<IActionResult> EditarEmpresa()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario.UsuarioIdEmpresa == null)
            {
                return BadRequest("El usuario no está asociado a ninguna empresa.");
            }

            var empresa = await _context.Empresas
                .Include(e => e.Aep) // Incluye los datos del sector económico si es necesario
                .FirstOrDefaultAsync(e => e.EmpId == usuario.UsuarioIdEmpresa);

            if (empresa == null)
            {
                return NotFound("Empresa no encontrada.");
            }

            ViewBag.SectoresPrincipales = await _context.AeSectoresPrincipales.ToListAsync();

            return View(empresa);
        }

        //POST: Guardar la edición del formulario de empresa
        [HttpPost]
        public async Task<IActionResult> EditarEmpresa(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario.UsuarioIdEmpresa == null)
            {
                //Badrequest sirve como mensaje
                return BadRequest("El usuario no está asociado a ninguna empresa.");
            }

            var empresaExistente = await _context.Empresas.FindAsync(usuario.UsuarioIdEmpresa);

            if (empresaExistente == null)
            {
                return NotFound("Empresa no encontrada.");
            }

            empresaExistente.EmpNombreEmpresa = empresa.EmpNombreEmpresa;
            empresaExistente.EmpEmailRegistro = empresa.EmpEmailRegistro;
            empresaExistente.EmpEmailAcceso = empresa.EmpEmailAcceso;
            empresaExistente.EmpPassword = empresa.EmpPassword;
            empresaExistente.EmpRuc = empresa.EmpRuc;
            empresaExistente.EmpRazonSocial = empresa.EmpRazonSocial;
            empresaExistente.EmpCiudad = empresa.EmpCiudad;
            empresaExistente.EmpTelefono = empresa.EmpTelefono;
            empresaExistente.EmpNumeroTrabajadores = empresa.EmpNumeroTrabajadores;
            empresaExistente.EmpVacantesAnuales = empresa.EmpVacantesAnuales;
            empresaExistente.EmpDescripcion = empresa.EmpDescripcion;
            empresaExistente.AepId = empresa.AepId; // Actualiza el sector económico principal

            await _context.SaveChangesAsync();


            return RedirectToAction("VerPostulaciones", "Empleador");
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

        // GET: Cargar formulario para editar oferta
        public async Task<IActionResult> EditarOferta(int id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            // Cargar los datos necesarios para los dropdowns
            ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
            ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
            ViewBag.OfertaModalidades = await _context.OfertaModalidads.ToListAsync();
            ViewBag.Provincias = await _context.Provincias.ToListAsync();
            ViewBag.Ciudades = await _context.Ciudades.ToListAsync();

            return View(oferta);
        }

        // POST: Actualizar oferta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarOferta(int id, Oferta oferta)
        {
            if (id != oferta.OfeId)
            {
                return BadRequest();
            }

            var ofertaExistente = await _context.Ofertas.AsNoTracking().FirstOrDefaultAsync(o => o.OfeId == id);

            if (ofertaExistente == null)
            {
                return NotFound();
            }

            oferta.EmpId = _context.Ofertas.AsNoTracking()
              .Where(o => o.OfeId == id)
              .Select(o => o.EmpId)
              .FirstOrDefault();

            // Preservar la fecha de publicación original
            oferta.OfeFechaPublicacion = ofertaExistente.OfeFechaPublicacion;

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar la oferta en la base de datos
                    _context.Update(oferta);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("VerPostulaciones", "Empleador"); // Redirigir a la vista principal del empleador
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaExists(oferta.OfeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Recargar los dropdowns en caso de error
            ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
            ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
            ViewBag.OfertaModalidades = await _context.OfertaModalidads.ToListAsync();
            ViewBag.Provincias = await _context.Provincias.ToListAsync();
            ViewBag.Ciudades = await _context.Ciudades.ToListAsync();

            return View(oferta);
        }

        private bool OfertaExists(int id)
        {
            return _context.Ofertas.Any(e => e.OfeId == id);
        }

        public async Task<IActionResult> VerPostulaciones(DateTime? fechaDesde, DateTime? fechaHasta, int? estadoPostulacion, bool? tienePostulantes,
                                                  int? provincia, int? ciudad, int? areaLaboral, int? puestoLaboral,
                                                  int? ofertaModalidad, decimal? salarioMinimo, decimal? salarioMaximo,
                                                  string? tipoContrato)
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

                var empresa = await _context.Empresas
                    .FirstOrDefaultAsync(e => e.EmpId == usuario.UsuarioIdEmpresa.Value);

                ViewBag.NombreEmpresa = empresa?.EmpNombreEmpresa;

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

                // Filtros adicionales
                if (provincia.HasValue)
                {
                    ofertas = ofertas.Where(o => o.ProId == provincia.Value);
                }

                if (ciudad.HasValue)
                {
                    ofertas = ofertas.Where(o => o.CidId == ciudad.Value);
                }

                if (areaLaboral.HasValue)
                {
                    ofertas = ofertas.Where(o => o.ArlId == areaLaboral.Value);
                }

                if (puestoLaboral.HasValue)
                {
                    ofertas = ofertas.Where(o => o.PulId == puestoLaboral.Value);
                }

                if (ofertaModalidad.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfmId == ofertaModalidad.Value);
                }

                if (salarioMinimo.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfeSalario >= salarioMinimo.Value);
                }

                if (salarioMaximo.HasValue)
                {
                    ofertas = ofertas.Where(o => o.OfeSalario <= salarioMaximo.Value);
                }

                if (!string.IsNullOrEmpty(tipoContrato))
                {
                    ofertas = ofertas.Where(o => o.OfeTipoContrato == tipoContrato);
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

                // Cargar listas de filtros
                ViewBag.EstadosPostulacion = await _context.EstadoPostulacions.ToListAsync();
                ViewBag.Provincias = await _context.Provincias.ToListAsync();
                ViewBag.Ciudades = await _context.Ciudades.ToListAsync();
                ViewBag.AreasLaborales = await _context.AreasLaborales.ToListAsync();
                ViewBag.PuestosLaborales = await _context.PuestosLaborales.ToListAsync();
                ViewBag.OfertasModalidades = await _context.OfertaModalidads.ToListAsync();

                return View(modelo);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> VerCandidatos(int ofertaId, int? edadDesde, int? edadHasta, string? genero, string? estadoCivil)
        {
            // Obtener la oferta con detalles relacionados
            var oferta = await _context.Ofertas
                .Include(o => o.Pro) // Incluye la provincia
                .Include(o => o.Cid) // Incluye la ciudad
                .FirstOrDefaultAsync(o => o.OfeId == ofertaId);

            if (oferta == null)
            {
                return NotFound();
            }

            // Obtener la lista de estados de postulación
            var estadoPostulacion = await _context.EstadoPostulacions
                .ToListAsync();

            // Obtener las postulaciones relacionadas
            var postulaciones = _context.Postulaciones
                .Include(p => p.Usu)
                .ThenInclude(u => u.UsuarioDetalles)
                .Include(p => p.Usu)
                .ThenInclude(u => u.UsuarioPerfils)
                .Where(p => p.OfeId == ofertaId)
                .AsQueryable();

            // Aplicar filtros por género
            if (!string.IsNullOrEmpty(genero))
            {
                postulaciones = postulaciones
                    .Where(p => p.Usu.UsuarioDetalles.Any(d => d.UsdGenero == genero));
            }

            // Aplicar filtros por edad
            if (edadDesde.HasValue || edadHasta.HasValue)
            {
                var fechaHoy = DateTime.Now;
                if (edadDesde.HasValue)
                {
                    var fechaDesde = fechaHoy.AddYears(-edadDesde.Value);
                    postulaciones = postulaciones
                        .Where(p => p.Usu.UsuarioDetalles.Any(d => d.UsdFechaNacimiento <= fechaDesde));
                }
                if (edadHasta.HasValue)
                {
                    var fechaHasta = fechaHoy.AddYears(-edadHasta.Value);
                    postulaciones = postulaciones
                        .Where(p => p.Usu.UsuarioDetalles.Any(d => d.UsdFechaNacimiento >= fechaHasta));
                }
            }


            var listaPostulaciones = await postulaciones.ToListAsync();

            // Crear el ViewModel
            var modelo = new CandidatosViewModel(
                ofertaId: ofertaId,
                nombreOferta: oferta.OfeTitulo,
                provinciaOferta: oferta.Pro.ProNombre,
                ciudadOferta: oferta.Cid.CidNombre,
                postulaciones: listaPostulaciones,
                estadosPostulacion: estadoPostulacion
            );

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int postulacionId, int nuevoEstado)
        {
            // Buscar la postulación e incluir la oferta asociada
            var postulacion = await _context.Postulaciones
                .Include(p => p.Ofe) // Asegúrate de que la oferta se cargue junto con la postulación
                .FirstOrDefaultAsync(p => p.PosId == postulacionId);

            if (postulacion == null)
            {
                return NotFound();
            }

            // Si el nuevo estado es "Aceptado" (EspId = 5)
            if (nuevoEstado == 5)
            {
                // Contar cuántas postulaciones ya tienen el estado "Aceptado" para la oferta actual
                int postulacionesAceptadas = await _context.Postulaciones
                    .Where(p => p.OfeId == postulacion.OfeId && p.EspId == 5) // Filtrar por oferta y estado "Aceptado"
                    .CountAsync();

                // Obtener la cantidad de vacantes de la oferta
                int cantidadVacantes = (int)postulacion.Ofe.OfeCantidadVacantes;

                // Verificar si ya se alcanzó el límite de vacantes aceptadas
                if (postulacionesAceptadas >= cantidadVacantes)
                {
                    // Mostrar un mensaje de error o advertencia
                    TempData["ErrorMessage"] = "No se puede aceptar a más candidatos. La oferta ya ha alcanzado el número máximo de vacantes.";

                    // Redirigir de vuelta a la vista de candidatos sin cambiar el estado
                    return RedirectToAction("VerCandidatos", new { ofertaId = postulacion.OfeId });
                }
            }

            // Cambiar el estado de la postulación
            postulacion.EspId = nuevoEstado;

            // Guardar los cambios en la base de datos
            _context.Update(postulacion);
            await _context.SaveChangesAsync();

            // Redirigir de vuelta a la vista de candidatos
            return RedirectToAction("VerCandidatos", new { ofertaId = postulacion.OfeId });
        }
    }
}
