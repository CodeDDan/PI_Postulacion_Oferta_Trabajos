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
    public class AeSubdivisionesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public AeSubdivisionesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: AeSubdivisiones
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.AeSubdivisions.Include(a => a.Aep);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: AeSubdivisiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AeSubdivisions == null)
            {
                return NotFound();
            }

            var aeSubdivision = await _context.AeSubdivisions
                .Include(a => a.Aep)
                .FirstOrDefaultAsync(m => m.AedId == id);
            if (aeSubdivision == null)
            {
                return NotFound();
            }

            return View(aeSubdivision);
        }

        // GET: AeSubdivisiones/Create
        public IActionResult Create()
        {
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepId");
            return View();
        }

        // POST: AeSubdivisiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AedId,AepId,AedNombre")] AeSubdivision aeSubdivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aeSubdivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepId", aeSubdivision.AepId);
            return View(aeSubdivision);
        }

        // GET: AeSubdivisiones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AeSubdivisions == null)
            {
                return NotFound();
            }

            var aeSubdivision = await _context.AeSubdivisions.FindAsync(id);
            if (aeSubdivision == null)
            {
                return NotFound();
            }
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepId", aeSubdivision.AepId);
            return View(aeSubdivision);
        }

        // POST: AeSubdivisiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AedId,AepId,AedNombre")] AeSubdivision aeSubdivision)
        {
            if (id != aeSubdivision.AedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aeSubdivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AeSubdivisionExists(aeSubdivision.AedId))
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
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepId", aeSubdivision.AepId);
            return View(aeSubdivision);
        }

        // GET: AeSubdivisiones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AeSubdivisions == null)
            {
                return NotFound();
            }

            var aeSubdivision = await _context.AeSubdivisions
                .Include(a => a.Aep)
                .FirstOrDefaultAsync(m => m.AedId == id);
            if (aeSubdivision == null)
            {
                return NotFound();
            }

            return View(aeSubdivision);
        }

        // POST: AeSubdivisiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AeSubdivisions == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.AeSubdivisions'  is null.");
            }
            var aeSubdivision = await _context.AeSubdivisions.FindAsync(id);
            if (aeSubdivision != null)
            {
                _context.AeSubdivisions.Remove(aeSubdivision);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AeSubdivisionExists(int id)
        {
          return (_context.AeSubdivisions?.Any(e => e.AedId == id)).GetValueOrDefault();
        }
    }
}
