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

    [Authorize(Roles = "admin")]
    public class EmpresasController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public EmpresasController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: Empresas
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.Empresas.Include(e => e.Aep);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Aep)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepNombre");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("EmpId,AepId,EmpNombreEmpresa,EmpEmailRegistro,EmpEmailAcceso,EmpPassword,EmpRuc,EmpRazonSocial,EmpCiudad,EmpTelefono,EmpNumeroTrabajadores,EmpVacantesAnuales,EmpDescripcion")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepNombre", empresa.AepId);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepNombre", empresa.AepId);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,AepId,EmpNombreEmpresa,EmpEmailRegistro,EmpEmailAcceso,EmpPassword,EmpRuc,EmpRazonSocial,EmpCiudad,EmpTelefono,EmpNumeroTrabajadores,EmpVacantesAnuales,EmpDescripcion")] Empresa empresa)
        {
            if (id != empresa.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.EmpId))
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
            ViewData["AepId"] = new SelectList(_context.AeSectoresPrincipales, "AepId", "AepNombre", empresa.AepId);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Aep)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresas == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.Empresas'  is null.");
            }
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
          return (_context.Empresas?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
