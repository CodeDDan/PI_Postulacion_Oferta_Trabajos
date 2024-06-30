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
    public class UsuariosDetallesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuariosDetallesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: UsuariosDetalles
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioDetalles.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuariosDetalles/Details/5
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

        // GET: UsuariosDetalles/Create
        public IActionResult Create()
        {
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId");
            return View();
        }

        // POST: UsuariosDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsdId,UsuId,UsdFechaNacimiento,UsdEstadoCivil,UsdFoto,UsdCiudad,UsdGenero")] UsuarioDetalle usuarioDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioDetalle.UsuId);
            return View(usuarioDetalle);
        }

        // GET: UsuariosDetalles/Edit/5
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
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioDetalle.UsuId);
            return View(usuarioDetalle);
        }

        // POST: UsuariosDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsdId,UsuId,UsdFechaNacimiento,UsdEstadoCivil,UsdFoto,UsdCiudad,UsdGenero")] UsuarioDetalle usuarioDetalle)
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
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioDetalle.UsuId);
            return View(usuarioDetalle);
        }

        // GET: UsuariosDetalles/Delete/5
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

        // POST: UsuariosDetalles/Delete/5
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
