using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    [Authorize]
    public class AreasLaboralesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public AreasLaboralesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: AreasLaborales
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
              return _context.AreasLaborales != null ? 
                          View(await _context.AreasLaborales.ToListAsync()) :
                          Problem("Entity set 'PO_TrabajosContext.AreasLaborales'  is null.");
        }

        // GET: AreasLaborales/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AreasLaborales == null)
            {
                return NotFound();
            }

            var areaLaboral = await _context.AreasLaborales
                .FirstOrDefaultAsync(m => m.ArlId == id);
            if (areaLaboral == null)
            {
                return NotFound();
            }

            return View(areaLaboral);
        }

        // GET: AreasLaborales/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AreasLaborales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ArlId,ArlNombre")] AreaLaboral areaLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(areaLaboral);
        }

        // GET: AreasLaborales/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AreasLaborales == null)
            {
                return NotFound();
            }

            var areaLaboral = await _context.AreasLaborales.FindAsync(id);
            if (areaLaboral == null)
            {
                return NotFound();
            }
            return View(areaLaboral);
        }

        // POST: AreasLaborales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ArlId,ArlNombre")] AreaLaboral areaLaboral)
        {
            if (id != areaLaboral.ArlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaLaboralExists(areaLaboral.ArlId))
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
            return View(areaLaboral);
        }

        // GET: AreasLaborales/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AreasLaborales == null)
            {
                return NotFound();
            }

            var areaLaboral = await _context.AreasLaborales
                .FirstOrDefaultAsync(m => m.ArlId == id);
            if (areaLaboral == null)
            {
                return NotFound();
            }

            return View(areaLaboral);
        }

        // POST: AreasLaborales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AreasLaborales == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.AreasLaborales'  is null.");
            }
            var areaLaboral = await _context.AreasLaborales.FindAsync(id);
            if (areaLaboral != null)
            {
                _context.AreasLaborales.Remove(areaLaboral);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaLaboralExists(int id)
        {
          return (_context.AreasLaborales?.Any(e => e.ArlId == id)).GetValueOrDefault();
        }

        // Método para la validación de unicidad del área laboral, se lo usa en el modelo
        [HttpGet]
        public async Task<IActionResult> ValidateUniqueAreaLaboral(string arlNombre, int? arlId)
        {
            // Consulta para verificar la unicidad
            bool exists = await _context.AreasLaborales
                .Where(a => a.ArlNombre == arlNombre && (!arlId.HasValue || a.ArlId != arlId.Value))
                .AnyAsync();

            if (exists)
            {
                return Json("El nombre del área laboral ya existe.");
            }

            return Json(true);
        }

        // Acción para obtener los puestos laborales basados en el ArlId
        [Authorize(Roles = "admin, empleador, trabajador")]
        public IActionResult GetPuestosLaborales(int arlId)
        {
            var puestos = _context.PuestosLaborales
                .Where(p => p.ArlId == arlId)
                .Select(p => new
                {
                    p.PulId,
                    p.PulNombre
                })
                .ToList();

            return Json(puestos);
        }
    }
}
