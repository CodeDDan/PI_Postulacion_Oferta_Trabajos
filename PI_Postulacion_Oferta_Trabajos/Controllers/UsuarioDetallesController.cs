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
    public class UsuarioDetallesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioDetallesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: UsuarioDetalles
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioDetalles.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuarioDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioDetalles == null)
            {
                return NotFound();
            }

            var usuarioDetalle = await _context.UsuarioDetalles
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsdId == id);
            if (usuarioDetalle == null)
            {
                return NotFound();
            }

            return View(usuarioDetalle);
        }

        // GET: UsuarioDetalles/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: UsuarioDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsdId,UsuarioId,UsdFechaNacimiento,UsdEstadoCivil,UsdFoto,UsdCiudad,UsdGenero")] UsuarioDetalle usuarioDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioDetalle.UsuarioId);
            return View(usuarioDetalle);
        }

        // GET: UsuarioDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioDetalles == null)
            {
                return NotFound();
            }

            var usuarioDetalle = await _context.UsuarioDetalles.FindAsync(id);
            if (usuarioDetalle == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioDetalle.UsuarioId);
            return View(usuarioDetalle);
        }

        // POST: UsuarioDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsdId,UsuarioId,UsdFechaNacimiento,UsdEstadoCivil,UsdFoto,UsdCiudad,UsdGenero")] UsuarioDetalle usuarioDetalle)
        {
            if (id != usuarioDetalle.UsdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioDetalleExists(usuarioDetalle.UsdId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioDetalle.UsuarioId);
            return View(usuarioDetalle);
        }

        // GET: UsuarioDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioDetalles == null)
            {
                return NotFound();
            }

            var usuarioDetalle = await _context.UsuarioDetalles
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsdId == id);
            if (usuarioDetalle == null)
            {
                return NotFound();
            }

            return View(usuarioDetalle);
        }

        // POST: UsuarioDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioDetalles == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.UsuarioDetalles'  is null.");
            }
            var usuarioDetalle = await _context.UsuarioDetalles.FindAsync(id);
            if (usuarioDetalle != null)
            {
                _context.UsuarioDetalles.Remove(usuarioDetalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioDetalleExists(int id)
        {
          return (_context.UsuarioDetalles?.Any(e => e.UsdId == id)).GetValueOrDefault();
        }
    }
}
