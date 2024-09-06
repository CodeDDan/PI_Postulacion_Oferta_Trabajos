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

        [HttpPost]
        public IActionResult EliminarSalario(string usuarioId)
        {
            // Obtener el usuario correspondiente en la base de datos
            var usuario = _context.UsuarioPerfils.FirstOrDefault(u => u.UsuarioId == usuarioId);

            if (usuario != null)
            {
                // Establecer el valor de UspPreferenciaSalarial como null
                usuario.UspPreferenciaSalarial = null;

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                // Devolver un estado de éxito
                return Json(new { success = true });
            }

            // Devolver un estado de error si el usuario no se encuentra
            return Json(new { success = false, message = "Usuario no encontrado." });
        }

        [HttpPost]
        public IActionResult EliminarDiscapacidad(string usuarioId)
        {
            // Obtener el usuario correspondiente en la base de datos
            var usuario = _context.UsuarioPerfils.FirstOrDefault(u => u.UsuarioId == usuarioId);

            if (usuario != null)
            {
                // Establecer el valor de UspPreferenciaSalarial como null
                usuario.UspDiscapacidad = null;

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                // Devolver un estado de éxito
                return Json(new { success = true });
            }

            // Devolver un estado de error si el usuario no se encuentra
            return Json(new { success = false, message = "Usuario no encontrado." });
        }
        [HttpPost]
        public IActionResult EliminarObjetivo(string usuarioId)
        {
            // Obtener el usuario correspondiente en la base de datos
            var usuario = _context.UsuarioPerfils.FirstOrDefault(u => u.UsuarioId == usuarioId);

            if (usuario != null)
            {
                // Establecer el valor de UspPreferenciaSalarial como null
                usuario.UspAspiracionLarboral = null;

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                // Devolver un estado de éxito
                return Json(new { success = true });
            }

            // Devolver un estado de error si el usuario no se encuentra
            return Json(new { success = false, message = "Usuario no encontrado." });
        }
        [HttpGet]
        public IActionResult ObtenerSalarioPorId(string usuarioId)
        {
            // Supongamos que tienes un contexto de base de datos o un servicio para acceder a los datos
            var usuario = _context.UsuarioPerfils
                .Where(u => u.UsuarioId == usuarioId)
                .Select(u => new {
                    u.UspPreferenciaSalarial,
                    u.UsuarioId,
                    u.UspAspiracionLarboral,
                    u.UspDiscapacidad
                })
                .FirstOrDefault();

            if (usuario == null)
            {
                return Json(new { success = false, message = "No se encontró el usuario." });
            }

            return Json(new { success = true, data = usuario });
        }

        public JsonResult GetPerfil(string usuarioId)
        {
            var experiencias = _context.UsuarioPerfils
                .Where(x => x.UsuarioId == usuarioId)
                .ToList();

            return Json(experiencias);
        }

        [HttpPost]
        public async Task<IActionResult> SaveObjetive(UsuarioPerfil usuarioPerfil)
        {
            // Busca el perfil de usuario por el UsuarioId (no es clave primaria)
            var objetive = await _context.UsuarioPerfils.FirstOrDefaultAsync(u => u.UsuarioId == usuarioPerfil.UsuarioId);

            // Si no existe, crea un nuevo registro
            if (objetive == null)
            {
                // Crea un nuevo objeto
                var newUsuarioPerfil = new UsuarioPerfil
                {
                    UsuarioId = usuarioPerfil.UsuarioId,
                    UspAspiracionLarboral = usuarioPerfil.UspAspiracionLarboral
                };

                // Agrega el nuevo perfil
                _context.UsuarioPerfils.Add(newUsuarioPerfil);
            }
            else
            {
                // Si el perfil ya existe, actualiza el objetivo laboral
                objetive.UspAspiracionLarboral = usuarioPerfil.UspAspiracionLarboral;
                _context.Update(objetive);
            }

            // Guarda los cambios
            await _context.SaveChangesAsync();

            // Devuelve una respuesta en formato JSON
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> SaveSalary(UsuarioPerfil usuarioPerfil)
        {
            // Busca el perfil de usuario por el UsuarioId (no es clave primaria)
            var existingProfile = await _context.UsuarioPerfils.FirstOrDefaultAsync(u => u.UsuarioId == usuarioPerfil.UsuarioId);

            // Si no existe, crea un nuevo registro
            if (existingProfile == null)
            {
                // Crea un nuevo objeto
                var newUsuarioPerfil = new UsuarioPerfil
                {
                    UsuarioId = usuarioPerfil.UsuarioId,
                    UspPreferenciaSalarial = usuarioPerfil.UspPreferenciaSalarial
                };

                // Agrega el nuevo perfil
                _context.UsuarioPerfils.Add(newUsuarioPerfil);
            }
            else
            {
                // Si el perfil ya existe, actualiza la preferencia salarial
                existingProfile.UspPreferenciaSalarial = usuarioPerfil.UspPreferenciaSalarial;
                _context.Update(existingProfile);
            }

            // Guarda los cambios
            await _context.SaveChangesAsync();

            // Devuelve una respuesta en formato JSON
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> SaveDisability(UsuarioPerfil usuarioPerfil)
        {
            // Busca el perfil de usuario por el UsuarioId (no es clave primaria)
            var existingProfile = await _context.UsuarioPerfils.FirstOrDefaultAsync(u => u.UsuarioId == usuarioPerfil.UsuarioId);

            // Si no existe, crea un nuevo registro
            if (existingProfile == null)
            {
                // Crea un nuevo objeto
                var newUsuarioPerfil = new UsuarioPerfil
                {
                    UsuarioId = usuarioPerfil.UsuarioId,
                    UspDiscapacidad = usuarioPerfil.UspDiscapacidad
                };

                // Agrega el nuevo perfil
                _context.UsuarioPerfils.Add(newUsuarioPerfil);
            }
            else
            {
                // Si el perfil ya existe, actualiza la discapacidad
                existingProfile.UspDiscapacidad = usuarioPerfil.UspDiscapacidad;
                _context.Update(existingProfile);
            }

            // Guarda los cambios
            await _context.SaveChangesAsync();

            // Devuelve una respuesta en formato JSON
            return Json(new { success = true });
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
