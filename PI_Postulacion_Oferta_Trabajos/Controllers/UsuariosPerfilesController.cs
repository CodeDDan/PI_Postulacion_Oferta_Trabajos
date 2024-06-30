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
    public class UsuariosPerfilesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuariosPerfilesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: UsuariosPerfiles
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioPerfils.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuariosPerfiles/Details/5
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

        // GET: UsuariosPerfiles/Create
        public IActionResult Create()
        {
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId");
            return View();
        }

        // POST: UsuariosPerfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UspId,UsuId,UspAspiracionLarboral,UspPreferenciaSalarial,UspHojaVida,UspDiscapacidad")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioPerfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioPerfil.UsuId);
            return View(usuarioPerfil);
        }

        // GET: UsuariosPerfiles/Edit/5
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
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioPerfil.UsuId);
            return View(usuarioPerfil);
        }

        // POST: UsuariosPerfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UspId,UsuId,UspAspiracionLarboral,UspPreferenciaSalarial,UspHojaVida,UspDiscapacidad")] UsuarioPerfil usuarioPerfil)
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
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioPerfil.UsuId);
            return View(usuarioPerfil);
        }

        // GET: UsuariosPerfiles/Delete/5
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

        // POST: UsuariosPerfiles/Delete/5
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
