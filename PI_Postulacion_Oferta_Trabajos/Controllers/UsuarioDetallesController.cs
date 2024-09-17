using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                // Validar datos
                if (!IsValidEmail(Email))
                {
                    return Json(new { success = false, message = "El correo electrónico no es válido." });
                }

                if (!IsValidPhoneNumber(PhoneNumber))
                {
                    return Json(new { success = false, message = "El número de teléfono no es válido." });
                }

                if (!IsValidCedula(Cedula))
                {
                    return Json(new { success = false, message = "La cédula no es válida." });
                }

                // Buscar el usuario actual
                var usuarioActual = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioDetalle.UsuarioId);

                if (usuarioActual == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado." });
                }

                // Verificar si el correo electrónico ya está en uso por otro usuario
                if (_context.Usuarios.Any(u => u.Email == Email && u.Id != usuarioDetalle.UsuarioId))
                {
                    return Json(new { success = false, message = "El correo electrónico ya está en uso por otro usuario." });
                }

                // Verificar si el número de teléfono ya está en uso por otro usuario
                if (_context.Usuarios.Any(u => u.PhoneNumber == PhoneNumber && u.Id != usuarioDetalle.UsuarioId))
                {
                    return Json(new { success = false, message = "El número de teléfono ya está en uso por otro usuario." });
                }

                // Verificar si la cédula ya está en uso por otro usuario
                if (_context.Usuarios.Any(u => u.UsuCedula == Cedula && u.Id != usuarioDetalle.UsuarioId))
                {
                    return Json(new { success = false, message = "La cédula ya está en uso por otro usuario." });
                }

                // Buscar si ya existe un UsuarioDetalle con el UsuarioId
                var usuarioExistente = _context.UsuarioDetalles
                    .Include(u => u.Usu) // Incluye los datos del Usuario relacionado
                    .FirstOrDefault(u => u.UsuarioId == usuarioDetalle.UsuarioId);

                // Buscar el usuario directamente por su ID
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioDetalle.UsuarioId);

                if (usuario != null)
                {
                    // Actualizar los datos del Usuario independientemente de si existe o no el UsuarioDetalle
                    usuario.Email = Email;
                    usuario.PhoneNumber = PhoneNumber;
                    usuario.UsuCedula = Cedula;
                    usuario.UserName = Email;
                    usuario.NormalizedEmail = Email.ToUpper();
                    usuario.NormalizedUserName = Email.ToUpper();
                }
                else
                {
                    // Si el usuario no existe, retorna un error
                    return Json(new { success = false, message = "Usuario no encontrado." });
                }

                if (usuarioExistente != null)
                {
                    // Si existe el UsuarioDetalle, actualizamos sus campos
                    usuarioExistente.UsdFechaNacimiento = usuarioDetalle.UsdFechaNacimiento;
                    usuarioExistente.UsdEstadoCivil = usuarioDetalle.UsdEstadoCivil;
                    usuarioExistente.UsdCiudad = usuarioDetalle.UsdCiudad;
                    usuarioExistente.UsdGenero = usuarioDetalle.UsdGenero;
                }
                else
                {
                    // Si no existe UsuarioDetalle, creamos uno nuevo
                    var nuevoUsuarioDetalle = new UsuarioDetalle
                    {
                        UsuarioId = usuarioDetalle.UsuarioId, // Asignamos el ID del usuario
                        UsdFechaNacimiento = usuarioDetalle.UsdFechaNacimiento,
                        UsdEstadoCivil = usuarioDetalle.UsdEstadoCivil,
                        UsdCiudad = usuarioDetalle.UsdCiudad,
                        UsdGenero = usuarioDetalle.UsdGenero
                    };

                    _context.UsuarioDetalles.Add(nuevoUsuarioDetalle);
                }

                // Guardar los cambios en la base de datos
                _context.SaveChanges();

                return Json(new { success = true, message = "Datos actualizados correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar los datos: " + ex.Message });
            }
        }


        // Método para validar el correo electrónico
        private bool IsValidEmail(string email)
        {
            // Expresión regular mejorada para validar correos electrónicos
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Método para validar el número de teléfono
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Expresión regular para validar números de teléfono ecuatorianos
            var phonePattern = @"^0[1-9]\d{8}$|^9\d{8}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }


        // Método para validar la cédula
        private bool IsValidCedula(string cedula)
        {
            // Verifica que la cédula tenga exactamente 10 dígitos
            if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            {
                return false;
            }

            // Convierte la cédula a una lista de enteros
            var cedulaNumeros = cedula.Select(c => int.Parse(c.ToString())).ToArray();

            // Verifica el primer dígito
            int primerDigito = cedulaNumeros[0];
            if (primerDigito < 1 || primerDigito > 6)
            {
                return false;
            }

            // Calcula el total según el algoritmo
            int suma = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 0) // Índices pares
                {
                    int valor = cedulaNumeros[i] * 2;
                    suma += valor > 9 ? valor - 9 : valor;
                }
                else // Índices impares
                {
                    suma += cedulaNumeros[i];
                }
            }

            // Calcula el dígito verificador
            int digitoVerificador = (10 - (suma % 10)) % 10;

            // Verifica que el dígito verificador coincide
            return digitoVerificador == cedulaNumeros[9];
        }

        [HttpGet]
        public IActionResult GetUserData(string userId)
        {
            try
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

                // Inicializa proId
                int? proId = null;

                if (usuarioDetalle != null)
                {
                    var ciudadNombre = usuarioDetalle.UsdCiudad;

                    if (!string.IsNullOrEmpty(ciudadNombre))
                    {
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

                    // Si la ciudad no se encuentra, asignar el primer proId de las provincias
                    if (proId == null && provincias.Any())
                    {
                        proId = provincias.First().ProId;
                    }
                }
                else
                {
                    // Si no existe usuarioDetalle, asignar el primer proId de las provincias
                    if (provincias.Any())
                    {
                        proId = provincias.First().ProId;
                    }
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
            catch (Exception ex)
            {
                // En caso de error, devolver valores nulos
                return Json(new
                {
                    usuarioDetalle = (object)null,
                    provincias = (object)null,
                    proId = (int?)null,
                    userInfo = (object)null
                });
            }
        }


        [HttpPost]
        public IActionResult UploadProfileImage(IFormFile file, string usuarioId)
        {
            if (file != null && file.Length > 0)
            {
                // Buscar si ya existe un UsuarioDetalle con el UsuarioId
                var usuario = _context.UsuarioDetalles.FirstOrDefault(u => u.UsuarioId == usuarioId);

                // Si no existe, crear un nuevo UsuarioDetalle
                if (usuario == null)
                {
                    usuario = new UsuarioDetalle
                    {
                        UsuarioId = usuarioId, // Asignamos el ID del usuario
                        UsdFoto = null
                    };

                    _context.UsuarioDetalles.Add(usuario);
                    _context.SaveChanges(); // Guardamos el nuevo UsuarioDetalle
                }

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

                // Actualizar la base de datos con el nuevo nombre de archivo
                usuario.UsdFoto = fileName;
                _context.SaveChanges();

                var imageUrl = Url.Content("~/fotoPerfil/" + fileName);
                return Json(new { success = true, imageUrl });
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
