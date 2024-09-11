﻿using System;
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
    public class PostulacionesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public PostulacionesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        // GET: Postulaciones
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var pO_TrabajosContext = _context.Postulaciones.Include(p => p.Esp).Include(p => p.Ofe).Include(p => p.Usu);
            return View(await pO_TrabajosContext.ToListAsync());
        }

        // GET: Postulaciones/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones
                .Include(p => p.Esp)
                .Include(p => p.Ofe)
                .Include(p => p.Usu)
                .FirstOrDefaultAsync(m => m.PosId == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // GET: Postulaciones/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["EspId"] = new SelectList(_context.EstadoPostulacions, "EspId", "EspId");
            ViewData["OfeId"] = new SelectList(_context.Ofertas, "OfeId", "OfeId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Postulaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PosId,OfeId,UsuarioId,EspId,PosFechaPostulacion")] Postulacion postulacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspId"] = new SelectList(_context.EstadoPostulacions, "EspId", "EspId", postulacion.EspId);
            ViewData["OfeId"] = new SelectList(_context.Ofertas, "OfeId", "OfeId", postulacion.OfeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", postulacion.UsuarioId);
            return View(postulacion);
        }

        // GET: Postulaciones/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null)
            {
                return NotFound();
            }
            ViewData["EspId"] = new SelectList(_context.EstadoPostulacions, "EspId", "EspId", postulacion.EspId);
            ViewData["OfeId"] = new SelectList(_context.Ofertas, "OfeId", "OfeId", postulacion.OfeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", postulacion.UsuarioId);
            return View(postulacion);
        }

        // POST: Postulaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PosId,OfeId,UsuarioId,EspId,PosFechaPostulacion")] Postulacion postulacion)
        {
            if (id != postulacion.PosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostulacionExists(postulacion.PosId))
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
            ViewData["EspId"] = new SelectList(_context.EstadoPostulacions, "EspId", "EspId", postulacion.EspId);
            ViewData["OfeId"] = new SelectList(_context.Ofertas, "OfeId", "OfeId", postulacion.OfeId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", postulacion.UsuarioId);
            return View(postulacion);
        }

        // GET: Postulaciones/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.Postulaciones
                .Include(p => p.Esp)
                .Include(p => p.Ofe)
                .Include(p => p.Usu)
                .FirstOrDefaultAsync(m => m.PosId == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // POST: Postulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Postulaciones == null)
            {
                return Problem("Entity set 'PO_TrabajosContext.Postulaciones'  is null.");
            }
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostulacionExists(int id)
        {
          return (_context.Postulaciones?.Any(e => e.PosId == id)).GetValueOrDefault();
        }
    }
}
