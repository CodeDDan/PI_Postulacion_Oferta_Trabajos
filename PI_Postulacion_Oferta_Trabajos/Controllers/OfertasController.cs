﻿using System;
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
    public class OfertasController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public OfertasController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: Ofertas
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.Ofertas.Include(o => o.Emp);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: Ofertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .Include(o => o.Emp)
                .FirstOrDefaultAsync(m => m.OfeId == id);
            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }

        // GET: Ofertas/Create
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpId");
            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfeId,EmpId,OfeTitulo,OfeDescripcion,OfeRequisitos,OfeSalario,OfeFechaPublicacion")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oferta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpId", oferta.EmpId);
            return View(oferta);
        }

        // GET: Ofertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpId", oferta.EmpId);
            return View(oferta);
        }

        // POST: Ofertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfeId,EmpId,OfeTitulo,OfeDescripcion,OfeRequisitos,OfeSalario,OfeFechaPublicacion")] Oferta oferta)
        {
            if (id != oferta.OfeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oferta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaExists(oferta.OfeId))
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
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpId", oferta.EmpId);
            return View(oferta);
        }

        // GET: Ofertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .Include(o => o.Emp)
                .FirstOrDefaultAsync(m => m.OfeId == id);
            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }

        // POST: Ofertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ofertas == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.Ofertas'  is null.");
            }
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta != null)
            {
                _context.Ofertas.Remove(oferta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaExists(int id)
        {
          return (_context.Ofertas?.Any(e => e.OfeId == id)).GetValueOrDefault();
        }
    }
}
