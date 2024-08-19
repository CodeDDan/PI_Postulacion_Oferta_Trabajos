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
    public class CiudadesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public CiudadesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.Ciudades.Include(c => c.Pro);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudades
                .Include(c => c.Pro)
                .FirstOrDefaultAsync(m => m.CidId == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre");
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CidId,ProId,CidNombre")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", ciudad.ProId);
            return View(ciudad);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudades.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", ciudad.ProId);
            return View(ciudad);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CidId,ProId,CidNombre")] Ciudad ciudad)
        {
            if (id != ciudad.CidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciudad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadExists(ciudad.CidId))
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
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", ciudad.ProId);
            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ciudades == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudades
                .Include(c => c.Pro)
                .FirstOrDefaultAsync(m => m.CidId == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ciudades == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.Ciudades'  is null.");
            }
            var ciudad = await _context.Ciudades.FindAsync(id);
            if (ciudad != null)
            {
                _context.Ciudades.Remove(ciudad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadExists(int id)
        {
          return (_context.Ciudades?.Any(e => e.CidId == id)).GetValueOrDefault();
        }

        // Método para la validación de unicidad de la ciudad
        [HttpGet]
        public async Task<IActionResult> ValidateUniqueCiudad(string cidNombre, int? cidId)
        {
            // Consulta para verificar la unicidad
            bool exists = await _context.Ciudades
                .Where(c => c.CidNombre == cidNombre && (!cidId.HasValue || c.CidId != cidId.Value))
                .AnyAsync();

            if (exists)
            {
                return Json("El nombre de la ciudad ya existe");
            }

            return Json(true);
        }
    }
}
