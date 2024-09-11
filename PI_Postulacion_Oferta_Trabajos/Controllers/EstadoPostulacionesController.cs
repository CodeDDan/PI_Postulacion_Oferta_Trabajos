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
    public class EstadoPostulacionesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public EstadoPostulacionesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: EstadoPostulaciones
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
              return _context.EstadoPostulacions != null ? 
                          View(await _context.EstadoPostulacions.ToListAsync()) :
                          Problem("Entity set 'PO_TrabajosContext.EstadoPostulacions'  is null.");
        }

        // GET: EstadoPostulaciones/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstadoPostulacions == null)
            {
                return NotFound();
            }

            var estadoPostulacion = await _context.EstadoPostulacions
                .FirstOrDefaultAsync(m => m.EspId == id);
            if (estadoPostulacion == null)
            {
                return NotFound();
            }

            return View(estadoPostulacion);
        }

        // GET: EstadoPostulaciones/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoPostulaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("EspId,EspNombre,EspDescripcion")] EstadoPostulacion estadoPostulacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoPostulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoPostulacion);
        }

        // GET: EstadoPostulaciones/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstadoPostulacions == null)
            {
                return NotFound();
            }

            var estadoPostulacion = await _context.EstadoPostulacions.FindAsync(id);
            if (estadoPostulacion == null)
            {
                return NotFound();
            }
            return View(estadoPostulacion);
        }

        // POST: EstadoPostulaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EspId,EspNombre,EspDescripcion")] EstadoPostulacion estadoPostulacion)
        {
            if (id != estadoPostulacion.EspId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoPostulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoPostulacionExists(estadoPostulacion.EspId))
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
            return View(estadoPostulacion);
        }

        // GET: EstadoPostulaciones/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstadoPostulacions == null)
            {
                return NotFound();
            }

            var estadoPostulacion = await _context.EstadoPostulacions
                .FirstOrDefaultAsync(m => m.EspId == id);
            if (estadoPostulacion == null)
            {
                return NotFound();
            }

            return View(estadoPostulacion);
        }

        // POST: EstadoPostulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstadoPostulacions == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.EstadoPostulacions'  is null.");
            }
            var estadoPostulacion = await _context.EstadoPostulacions.FindAsync(id);
            if (estadoPostulacion != null)
            {
                _context.EstadoPostulacions.Remove(estadoPostulacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoPostulacionExists(int id)
        {
          return (_context.EstadoPostulacions?.Any(e => e.EspId == id)).GetValueOrDefault();
        }
    }
}
