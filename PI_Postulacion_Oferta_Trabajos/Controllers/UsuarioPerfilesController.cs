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
    public class UsuarioPerfilesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioPerfilesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: UsuarioPerfiles
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioPerfils.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuarioPerfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UspId == id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }

            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfiles/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: UsuarioPerfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UspId,UsuarioId,UspAspiracionLarboral,UspPreferenciaSalarial,UspHojaVida,UspDiscapacidad")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioPerfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioPerfil.UsuarioId);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils.FindAsync(id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioPerfil.UsuarioId);
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UspId,UsuarioId,UspAspiracionLarboral,UspPreferenciaSalarial,UspHojaVida,UspDiscapacidad")] UsuarioPerfil usuarioPerfil)
        {
            if (id != usuarioPerfil.UspId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioPerfil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioPerfilExists(usuarioPerfil.UspId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioPerfil.UsuarioId);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioPerfils == null)
            {
                return NotFound();
            }

            var usuarioPerfil = await _context.UsuarioPerfils
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UspId == id);
            if (usuarioPerfil == null)
            {
                return NotFound();
            }

            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioPerfils == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.UsuarioPerfils'  is null.");
            }
            var usuarioPerfil = await _context.UsuarioPerfils.FindAsync(id);
            if (usuarioPerfil != null)
            {
                _context.UsuarioPerfils.Remove(usuarioPerfil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioPerfilExists(int id)
        {
          return (_context.UsuarioPerfils?.Any(e => e.UspId == id)).GetValueOrDefault();
        }
    }
}
