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
