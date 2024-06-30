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
    public class UsuariosExperienciasLaboralesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuariosExperienciasLaboralesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: UsuariosExperienciasLaborales
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.UsuarioExperienciaLaborals.Include(u => u.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: UsuariosExperienciasLaborales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsxId == id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }

            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuariosExperienciasLaborales/Create
        public IActionResult Create()
        {
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId");
            return View();
        }

        // POST: UsuariosExperienciasLaborales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsxId,UsuId,UsxEmpresa,UsxArea,UsxPuesto,UsxFechaInicio,UsxFechaFin,UsxNivelExperiencia")] UsuarioExperienciaLaboral usuarioExperienciaLaboral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioExperienciaLaboral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioExperienciaLaboral.UsuId);
            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuariosExperienciasLaborales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals.FindAsync(id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioExperienciaLaboral.UsuId);
            return View(usuarioExperienciaLaboral);
        }

        // POST: UsuariosExperienciasLaborales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsxId,UsuId,UsxEmpresa,UsxArea,UsxPuesto,UsxFechaInicio,UsxFechaFin,UsxNivelExperiencia")] UsuarioExperienciaLaboral usuarioExperienciaLaboral)
        {
            if (id != usuarioExperienciaLaboral.UsxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioExperienciaLaboral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExperienciaLaboralExists(usuarioExperienciaLaboral.UsxId))
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
            ViewData["UsuId"] = new SelectList(_context.Usuarios, "UsuId", "UsuId", usuarioExperienciaLaboral.UsuId);
            return View(usuarioExperienciaLaboral);
        }

        // GET: UsuariosExperienciasLaborales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioExperienciaLaborals == null)
            {
                return NotFound();
            }

            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals
                .Include(u => u.Usu)
                .FirstOrDefaultAsync(m => m.UsxId == id);
            if (usuarioExperienciaLaboral == null)
            {
                return NotFound();
            }

            return View(usuarioExperienciaLaboral);
        }

        // POST: UsuariosExperienciasLaborales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioExperienciaLaborals == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.UsuarioExperienciaLaborals'  is null.");
            }
            var usuarioExperienciaLaboral = await _context.UsuarioExperienciaLaborals.FindAsync(id);
            if (usuarioExperienciaLaboral != null)
            {
                _context.UsuarioExperienciaLaborals.Remove(usuarioExperienciaLaboral);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExperienciaLaboralExists(int id)
        {
          return (_context.UsuarioExperienciaLaborals?.Any(e => e.UsxId == id)).GetValueOrDefault();
        }
    }
}
