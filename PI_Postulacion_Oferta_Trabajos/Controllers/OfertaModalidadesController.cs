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
    public class OfertaModalidadesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public OfertaModalidadesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: OfertaModalidades
        public async Task<IActionResult> Index()
        {
              return _context.OfertaModalidads != null ? 
                          View(await _context.OfertaModalidads.ToListAsync()) :
                          Problem("Entity set 'PO_TrabajosContext.OfertaModalidads'  is null.");
        }

        // GET: OfertaModalidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OfertaModalidads == null)
            {
                return NotFound();
            }

            var ofertaModalidad = await _context.OfertaModalidads
                .FirstOrDefaultAsync(m => m.OfmId == id);
            if (ofertaModalidad == null)
            {
                return NotFound();
            }

            return View(ofertaModalidad);
        }

        // GET: OfertaModalidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfertaModalidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfmId,OfmNombre")] OfertaModalidad ofertaModalidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ofertaModalidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ofertaModalidad);
        }

        // GET: OfertaModalidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OfertaModalidads == null)
            {
                return NotFound();
            }

            var ofertaModalidad = await _context.OfertaModalidads.FindAsync(id);
            if (ofertaModalidad == null)
            {
                return NotFound();
            }
            return View(ofertaModalidad);
        }

        // POST: OfertaModalidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfmId,OfmNombre")] OfertaModalidad ofertaModalidad)
        {
            if (id != ofertaModalidad.OfmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofertaModalidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaModalidadExists(ofertaModalidad.OfmId))
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
            return View(ofertaModalidad);
        }

        // GET: OfertaModalidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OfertaModalidads == null)
            {
                return NotFound();
            }

            var ofertaModalidad = await _context.OfertaModalidads
                .FirstOrDefaultAsync(m => m.OfmId == id);
            if (ofertaModalidad == null)
            {
                return NotFound();
            }

            return View(ofertaModalidad);
        }

        // POST: OfertaModalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OfertaModalidads == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.OfertaModalidads'  is null.");
            }
            var ofertaModalidad = await _context.OfertaModalidads.FindAsync(id);
            if (ofertaModalidad != null)
            {
                _context.OfertaModalidads.Remove(ofertaModalidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaModalidadExists(int id)
        {
          return (_context.OfertaModalidads?.Any(e => e.OfmId == id)).GetValueOrDefault();
        }
    }
}
