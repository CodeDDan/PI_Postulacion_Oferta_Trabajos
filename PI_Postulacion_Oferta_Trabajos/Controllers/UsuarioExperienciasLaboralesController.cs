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
    public class UsuarioExperienciasLaboralesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioExperienciasLaboralesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveExperience(UsuarioExperienciaLaboral experienciaLaboral)
        {
            try
            {
                // Añadir la nueva experiencia laboral a la base de datos
                _context.UsuarioExperienciaLaborals.Add(experienciaLaboral);
                await _context.SaveChangesAsync();

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(new { success = false, message = "Datos no válidos." });
            }

        }

        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            var experiencia = _context.UsuarioExperienciaLaborals
                .FirstOrDefault(e => e.UsxId == id);

            if (experiencia != null)
            {
                _context.UsuarioExperienciaLaborals.Remove(experiencia);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult EditExperience(UsuarioExperienciaLaboral model)
        {
            try { 

                var experiencia = _context.UsuarioExperienciaLaborals
                    .FirstOrDefault(e => e.UsxId == model.UsxId);

                if (experiencia != null)
                {
                    experiencia.UsxEmpresa = model.UsxEmpresa;
                    experiencia.UsxArea = model.UsxArea;
                    experiencia.UsxPuesto = model.UsxPuesto;
                    experiencia.UsxFechaInicio = model.UsxFechaInicio;
                    experiencia.UsxFechaFin = model.UsxFechaFin;
                    experiencia.UsxNivelExperiencia = model.UsxNivelExperiencia;

                    _context.SaveChanges();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public IActionResult GetExperienceById(int id)
        {
            var experiencia = _context.UsuarioExperienciaLaborals
                .FirstOrDefault(e => e.UsxId == id);

            if (experiencia == null)
                return NotFound();

            return Json(experiencia);
        }
        [HttpGet]
        public JsonResult GetExperienciasLaborales(string usuarioId)
        {
            var experiencias = _context.UsuarioExperienciaLaborals
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();

            return Json(experiencias);
        }
        // GET: UsuarioExperienciasLaborales
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioExperienciaLaborals.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuarioExperienciasLaborales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsxId == id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }

            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuarioExperienciasLaborales/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: UsuarioExperienciasLaborales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsxId,UsuarioId,UsxEmpresa,UsxArea,UsxPuesto,UsxFechaInicio,UsxFechaFin,UsxNivelExperiencia")] UsuarioExperienciaLaboral usuarioExperienciaLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioExperienciaLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioExperienciaLaboral.UsuarioId);
            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuarioExperienciasLaborales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals.FindAsync(id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioExperienciaLaboral.UsuarioId);
            return View(usuarioExperienciaLaboral);
        }

        // POST: UsuarioExperienciasLaborales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsxId,UsuarioId,UsxEmpresa,UsxArea,UsxPuesto,UsxFechaInicio,UsxFechaFin,UsxNivelExperiencia")] UsuarioExperienciaLaboral usuarioExperienciaLaboral)
        {
            if (id != usuarioExperienciaLaboral.UsxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioExperienciaLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExperienciaLaboralExists(usuarioExperienciaLaboral.UsxId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioExperienciaLaboral.UsuarioId);
            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuarioExperienciasLaborales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsxId == id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }

            return View(usuarioExperienciaLaboral);
        }

        // POST: UsuarioExperienciasLaborales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioExperienciaLaborals == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.UsuarioExperienciaLaborals'  is null.");
            }
            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals.FindAsync(id);
            if (usuarioExperienciaLaboral != null)
            {
                _context.UsuarioExperienciaLaborals.Remove(usuarioExperienciaLaboral);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExperienciaLaboralExists(int id)
        {
          return (_context.UsuarioExperienciaLaborals?.Any(e => e.UsxId == id)).GetValueOrDefault();
        }
    }
}
