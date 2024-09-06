using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class UsuarioEducacionesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioEducacionesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEducacion([Bind("UsuarioId,USE_TIPO_FORMACION,USE_DOCUMENTO,USE_TITULO,USE_AREA,USE_INSTITUCION,USE_ESTADO,USE_FECHAI,USE_FECHAF")] UsuarioEducacion educacion)
        {

            try
            {
                // Agrega la nueva educación al contexto
                _context.UsuarioEducaciones.Add(educacion);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Educación añadida exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(new { success = false, message = "Error al agregar la educación: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetEducacion(string usuarioId)
        {
            var educaciones = _context.UsuarioEducaciones
                .Where(e => e.UsuarioId == usuarioId)
                .Select(e => new
                {
                    e.UseId,
                    e.USE_TITULO,
                    e.USE_TIPO_FORMACION,
                    e.USE_AREA,
                    e.USE_ESTADO,
                    e.USE_FECHAI,
                    e.USE_FECHAF,
                    e.USE_INSTITUCION
                })
                .ToList();

            return Json(educaciones);
        }
        [HttpGet]
        public IActionResult GetEducacionEdit(int usuarioId)
        {
            var educaciones = _context.UsuarioEducaciones
                .Where(e => e.UseId == usuarioId)
                .Select(e => new
                {
                    e.UseId,
                    e.USE_TITULO,
                    e.USE_TIPO_FORMACION,
                    e.USE_AREA,
                    e.USE_ESTADO,
                    e.USE_FECHAI,
                    e.USE_FECHAF,
                    e.USE_INSTITUCION
                })
                .ToList();

            return Json(educaciones);
        }
        [HttpPost]
        public IActionResult UpdateEducacion(IFormCollection form)
        {
            int useId = int.Parse(form["UseId"]);

            // Busca el registro existente en la base de datos
            var educacion = _context.UsuarioEducaciones.FirstOrDefault(e => e.UseId == useId);

            if (educacion == null)
            {
                return NotFound();
            }

            // Asigna los valores del formulario a las propiedades del modelo
            educacion.USE_TITULO = form["USE_TITULOEdit"];
            educacion.USE_TIPO_FORMACION = form["USE_TIPO_FORMACIONEdit"];
            educacion.USE_AREA = form["USE_AREAEdit"];
            educacion.USE_ESTADO = form["USE_ESTADOEdit"];
            educacion.USE_INSTITUCION = form["USE_INSTITUCIONEdit"];
            educacion.USE_FECHAI = form["USE_FECHAIEdit"];
            educacion.USE_FECHAF = form["USE_FECHAFEdit"];

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            // Retorna una respuesta exitosa
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarEstudio(int UseId)
        {
            if (UseId <= 0)
            {
                return Json(new { success = false, message = "ID inválido" });
            }

            try
            {
                // Busca el estudio usando una consulta LINQ
                var estudio = await _context.UsuarioEducaciones
                    .Where(e => e.UseId == UseId)
                    .FirstOrDefaultAsync();

                if (estudio == null)
                {
                    return Json(new { success = false, message = "Estudio no encontrado" });
                }

                // Elimina el estudio
                _context.UsuarioEducaciones.Remove(estudio);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el estudio: " + ex.Message });
            }
        }



        // GET: UsuarioEducaciones
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioEducacions.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuarioEducaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioEducacions == null)
            {
                return NotFound();
            }

            var usuarioEducacion = await _context.UsuarioEducacions
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UseId == id);
            if (usuarioEducacion == null)
            {
                return NotFound();
            }

            return View(usuarioEducacion);
        }

        // GET: UsuarioEducaciones/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: UsuarioEducaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UseId,UsuarioId,UseTipoFormacion,UseDocumento,UseDescripcion")] UsuarioEducacion usuarioEducacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioEducacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioEducacion.UsuarioId);
            return View(usuarioEducacion);
        }

        // GET: UsuarioEducaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioEducacions == null)
            {
                return NotFound();
            }

            var usuarioEducacion = await _context.UsuarioEducacions.FindAsync(id);
            if (usuarioEducacion == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioEducacion.UsuarioId);
            return View(usuarioEducacion);
        }

        // POST: UsuarioEducaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UseId,UsuarioId,UseTipoFormacion,UseDocumento,UseDescripcion")] UsuarioEducacion usuarioEducacion)
        {
            if (id != usuarioEducacion.UseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioEducacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioEducacionExists(usuarioEducacion.UseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioEducacion.UsuarioId);
            return View(usuarioEducacion);
        }

        // GET: UsuarioEducaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioEducacions == null)
            {
                return NotFound();
            }

            var usuarioEducacion = await _context.UsuarioEducacions
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UseId == id);
            if (usuarioEducacion == null)
            {
                return NotFound();
            }

            return View(usuarioEducacion);
        }

        // POST: UsuarioEducaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioEducacions == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.UsuarioEducacions'  is null.");
            }
            var usuarioEducacion = await _context.UsuarioEducacions.FindAsync(id);
            if (usuarioEducacion != null)
            {
                _context.UsuarioEducacions.Remove(usuarioEducacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioEducacionExists(int id)
        {
            return (_context.UsuarioEducacions?.Any(e => e.UseId == id)).GetValueOrDefault();
        }
    }
}
