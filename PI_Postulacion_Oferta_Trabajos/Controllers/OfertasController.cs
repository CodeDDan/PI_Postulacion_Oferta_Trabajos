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
            var pO_TrabajosContext = _context.Ofertas.Include(o => o.Arl).Include(o => o.Cid).Include(o => o.Emp).Include(o => o.Ofm).Include(o => o.Pro).Include(o => o.Pul);
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
                .Include(o => o.Arl)
                .Include(o => o.Cid)
                .Include(o => o.Emp)
                .Include(o => o.Ofm)
                .Include(o => o.Pro)
                .Include(o => o.Pul)
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
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlNombre");
            ViewData["CidId"] = new SelectList(_context.Ciudades, "CidId", "CidNombre");
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpNombreEmpresa");
            ViewData["OfmId"] = new SelectList(_context.OfertaModalidads, "OfmId", "OfmNombre");
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre");
            ViewData["PulId"] = new SelectList(_context.PuestosLaborales, "PulId", "PulNombre");
            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfeId,EmpId,OfmId,PulId,CidId,ArlId,ProId,OfeTitulo,OfeDescripcion,OfeTipoContrato,OfeSalario,OfeFechaPublicacion,OfeCantidadVacantes,OfeTiempoExperiencia,OfeEducacionMinima,OfeLicencia,OfeDisponibilidadViajar,OfeDisponibilidadCambioResidencia,OfeDiscapacidad,OfeEdadMinima")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oferta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlNombre", oferta.ArlId);
            ViewData["CidId"] = new SelectList(_context.Ciudades, "CidId", "CidNombre", oferta.CidId);
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpNombreEmpresa", oferta.EmpId);
            ViewData["OfmId"] = new SelectList(_context.OfertaModalidads, "OfmId", "OfmNombre", oferta.OfmId);
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", oferta.ProId);
            ViewData["PulId"] = new SelectList(_context.PuestosLaborales, "PulId", "PulNombre", oferta.PulId);
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
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlNombre", oferta.ArlId);
            ViewData["CidId"] = new SelectList(_context.Ciudades, "CidId", "CidNombre", oferta.CidId);
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpNombreEmpresa", oferta.EmpId);
            ViewData["OfmId"] = new SelectList(_context.OfertaModalidads, "OfmId", "OfmNombre", oferta.OfmId);
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", oferta.ProId);
            ViewData["PulId"] = new SelectList(_context.PuestosLaborales, "PulId", "PulNombre", oferta.PulId);
            return View(oferta);
        }

        // POST: Ofertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfeId,EmpId,OfmId,PulId,CidId,ArlId,ProId,OfeTitulo,OfeDescripcion,OfeTipoContrato,OfeSalario,OfeFechaPublicacion,OfeCantidadVacantes,OfeTiempoExperiencia,OfeEducacionMinima,OfeLicencia,OfeDisponibilidadViajar,OfeDisponibilidadCambioResidencia,OfeDiscapacidad,OfeEdadMinima")] Oferta oferta)
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
            ViewData["ArlId"] = new SelectList(_context.AreasLaborales, "ArlId", "ArlNombre", oferta.ArlId);
            ViewData["CidId"] = new SelectList(_context.Ciudades, "CidId", "CidNombre", oferta.CidId);
            ViewData["EmpId"] = new SelectList(_context.Empresas, "EmpId", "EmpNombreEmpresa", oferta.EmpId);
            ViewData["OfmId"] = new SelectList(_context.OfertaModalidads, "OfmId", "OfmNombre", oferta.OfmId);
            ViewData["ProId"] = new SelectList(_context.Provincias, "ProId", "ProNombre", oferta.ProId);
            ViewData["PulId"] = new SelectList(_context.PuestosLaborales, "PulId", "PulNombre", oferta.PulId);
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
                .Include(o => o.Arl)
                .Include(o => o.Cid)
                .Include(o => o.Emp)
                .Include(o => o.Ofm)
                .Include(o => o.Pro)
                .Include(o => o.Pul)
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
