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
    public class PuestosLaboralesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public PuestosLaboralesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: PuestosLaborales
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.PuestosLaborales.Include(p => p.Arl);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: PuestosLaborales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PuestosLaborales == null)
            {
                return NotFound();
            }

            var puestoLaboral = await _context.PuestosLaborales
                .Include(p => p.Arl)
                .FirstOrDefaultAsync(m => m.PulId == id);
            if (puestoLaboral == null)
            {
                return NotFound();
            }

            return View(puestoLaboral);
        }

        // GET: PuestosLaborales/Create
        public IActionResult Create()
        {
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlId");
            return View();
        }

        // POST: PuestosLaborales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PulId,ArlId,PulNombre")] PuestoLaboral puestoLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puestoLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlId", puestoLaboral.ArlId);
            return View(puestoLaboral);
        }

        // GET: PuestosLaborales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PuestosLaborales == null)
            {
                return NotFound();
            }

            var puestoLaboral = await _context.PuestosLaborales.FindAsync(id);
            if (puestoLaboral == null)
            {
                return NotFound();
            }
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlId", puestoLaboral.ArlId);
            return View(puestoLaboral);
        }

        // POST: PuestosLaborales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PulId,ArlId,PulNombre")] PuestoLaboral puestoLaboral)
        {
            if (id != puestoLaboral.PulId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puestoLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoLaboralExists(puestoLaboral.PulId))
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
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlId", puestoLaboral.ArlId);
            return View(puestoLaboral);
        }

        // GET: PuestosLaborales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PuestosLaborales == null)
            {
                return NotFound();
            }

            var puestoLaboral = await _context.PuestosLaborales
                .Include(p => p.Arl)
                .FirstOrDefaultAsync(m => m.PulId == id);
            if (puestoLaboral == null)
            {
                return NotFound();
            }

            return View(puestoLaboral);
        }

        // POST: PuestosLaborales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PuestosLaborales == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.PuestosLaborales'  is null.");
            }
            var puestoLaboral = await _context.PuestosLaborales.FindAsync(id);
            if (puestoLaboral != null)
            {
                _context.PuestosLaborales.Remove(puestoLaboral);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoLaboralExists(int id)
        {
          return (_context.PuestosLaborales?.Any(e => e.PulId == id)).GetValueOrDefault();
        }
    }
}
