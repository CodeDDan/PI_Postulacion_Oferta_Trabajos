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
    public class AeSectoresPrincipalesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public AeSectoresPrincipalesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: AeSectoresPrincipales
        public async Task<IActionResult> Index()
        {
              return _context.AeSectoresPrincipales != null ? 
                          View(await _context.AeSectoresPrincipales.ToListAsync()) :
                          Problem("Entity set 'PO_TrabajosContext.AeSectoresPrincipales'  is null.");
        }

        // GET: AeSectoresPrincipales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AeSectoresPrincipales == null)
            {
                return NotFound();
            }

            var aeSectorPrincipal = await _context.AeSectoresPrincipales
                .FirstOrDefaultAsync(m => m.AepId == id);
            if (aeSectorPrincipal == null)
            {
                return NotFound();
            }

            return View(aeSectorPrincipal);
        }

        // GET: AeSectoresPrincipales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AeSectoresPrincipales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AepId,AepNombre")] AeSectorPrincipal aeSectorPrincipal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aeSectorPrincipal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aeSectorPrincipal);
        }

        // GET: AeSectoresPrincipales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AeSectoresPrincipales == null)
            {
                return NotFound();
            }

            var aeSectorPrincipal = await _context.AeSectoresPrincipales.FindAsync(id);
            if (aeSectorPrincipal == null)
            {
                return NotFound();
            }
            return View(aeSectorPrincipal);
        }

        // POST: AeSectoresPrincipales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AepId,AepNombre")] AeSectorPrincipal aeSectorPrincipal)
        {
            if (id != aeSectorPrincipal.AepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aeSectorPrincipal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AeSectorPrincipalExists(aeSectorPrincipal.AepId))
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
            return View(aeSectorPrincipal);
        }

        // GET: AeSectoresPrincipales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AeSectoresPrincipales == null)
            {
                return NotFound();
            }

            var aeSectorPrincipal = await _context.AeSectoresPrincipales
                .FirstOrDefaultAsync(m => m.AepId == id);
            if (aeSectorPrincipal == null)
            {
                return NotFound();
            }

            return View(aeSectorPrincipal);
        }

        // POST: AeSectoresPrincipales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AeSectoresPrincipales == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.AeSectoresPrincipales'  is null.");
            }
            var aeSectorPrincipal = await _context.AeSectoresPrincipales.FindAsync(id);
            if (aeSectorPrincipal != null)
            {
                _context.AeSectoresPrincipales.Remove(aeSectorPrincipal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AeSectorPrincipalExists(int id)
        {
          return (_context.AeSectoresPrincipales?.Any(e => e.AepId == id)).GetValueOrDefault();
        }
    }
}
