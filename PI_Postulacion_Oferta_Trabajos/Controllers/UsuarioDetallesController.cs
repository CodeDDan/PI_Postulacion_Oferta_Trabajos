using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{

    [Authorize]
    public class UsuarioDetallesController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioDetallesController(PO_TrabajosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UpdateUserData(UsuarioDetalle usuarioDetalle, string Email, string PhoneNumber, string Cedula)
        {
            
            try
            {
                // Obtener el usuario existente por su ID
                var usuarioExistente = _context.UsuarioDetalles
                    .Include(u => u.Usu)
                    .FirstOrDefault(u => u.UsuarioId == usuarioDetalle.UsuarioId);

                if (usuarioExistente != null)
                {
                    // Actualizar los campos de UsuarioDetalle
                    usuarioExistente.UsdFechaNacimiento = usuarioDetalle.UsdFechaNacimiento;
                    usuarioExistente.UsdEstadoCivil = usuarioDetalle.UsdEstadoCivil;
                    usuarioExistente.UsdCiudad = usuarioDetalle.UsdCiudad;
                    usuarioExistente.UsdGenero = usuarioDetalle.UsdGenero;

                    // Actualizar los campos de Usuario (Email, PhoneNumber y Cedula)
                    usuarioExistente.Usu.Email = Email;
                    usuarioExistente.Usu.PhoneNumber = PhoneNumber;
                    usuarioExistente.Usu.UsuCedula = Cedula;

                    // Guardar cambios en la base de datos
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Datos actualizados correctamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Usuario no encontrado." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar los datos: " + ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetUserData(string userId)
        {
            // Obtener información del usuario desde UsuarioDetalles, si existe
            var usuarioDetalle = _context.UsuarioDetalles
                .Where(u => u.UsuarioId == userId)
                .Select(u => new
                {
                    u.UsuarioId,
                    u.UsdCiudad,
                    u.UsdGenero,
                    u.UsdEstadoCivil,
                    u.UsdFechaNacimiento
                })
                .FirstOrDefault();

            // Obtener provincias siempre
            var provincias = _context.Provincias
                .Select(p => new
                {
                    p.ProId,
                    p.ProNombre
                })
                .ToList();

            int? proId = null;

            if (usuarioDetalle != null)
            {
                var ciudadNombre = usuarioDetalle.UsdCiudad;

                // Obtener la provincia de la ciudad, si existe
                var ciudad = _context.Ciudades
                    .Where(c => c.CidNombre == ciudadNombre)
                    .Select(c => new
                    {
                        c.CidId,
                        c.CidNombre,
                        c.ProId
                    })
                    .FirstOrDefault();

                // Obtener el ID de la provincia asociada, si la ciudad fue encontrada
                proId = ciudad?.ProId;
            }

            // Obtener información del usuario desde AspNetUsers
            var userInfo = _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Email,
                    u.PhoneNumber,
                    u.UsuCedula,
                    u.UsuNombre,
                    u.UsuApellido
                })
                .FirstOrDefault();

            // Combina la información
            return Json(new { usuarioDetalle, provincias, proId, userInfo });
        }





        [HttpPost]
        public IActionResult UploadProfileImage(IFormFile file, string usuarioId)
        {
            if (file != null && file.Length > 0)
            {
                var usuario = _context.UsuarioDetalles.FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuario != null)
                {
                    // Ruta donde se almacenan las imágenes
                    var photoDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fotoPerfil");

                    // Si el usuario ya tiene una foto de perfil, eliminarla
                    if (!string.IsNullOrEmpty(usuario.UsdFoto))
                    {
                        var oldFilePath = Path.Combine(photoDirectory, usuario.UsdFoto);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Guardar la nueva foto de perfil
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{extension}";
                    var filePath = Path.Combine(photoDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Actualizar la base de datos
                    usuario.UsdFoto = fileName;
                    _context.SaveChanges();

                    var imageUrl = Url.Content("~/fotoPerfil/" + fileName);
                    return Json(new { success = true, imageUrl });
                }
            }

            return Json(new { success = false, message = "Error al subir la imagen" });
        }
        [HttpGet]
        public IActionResult GetProfileImageUrl(string usuarioId)
{
    var usuarioDetalle = _context.UsuarioDetalles
        .Where(u => u.UsuarioId == usuarioId)
        .Select(u => u.UsdFoto)
        .FirstOrDefault();

    // Asigna la imagen por defecto si no hay una imagen guardada en la base de datos
    var imageUrl = string.IsNullOrEmpty(usuarioDetalle)
        ? Url.Content("~/images/profile-placeholder.png")
        : Url.Content("~/fotoPerfil/" + usuarioDetalle);

    return Json(new { url = imageUrl });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("UsdId,UsuarioId,UsdFechaNacimiento,UsdEstadoCivil,UsdCiudad,UsdGenero")] UsuarioDetalle usuarioDetalle,
            IFormFile usdFoto) // Agregar el parámetro para el archivo
        {
            if (ModelState.IsValid)
            {
                // Maneja la carga del archivo
                if (usdFoto != null && usdFoto.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(usdFoto.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await usdFoto.CopyToAsync(stream);
                    }
                    usuarioDetalle.UsdFoto = "/images/" + Path.GetFileName(usdFoto.FileName); // Guarda la ruta del archivo en la base de datos
                }

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
