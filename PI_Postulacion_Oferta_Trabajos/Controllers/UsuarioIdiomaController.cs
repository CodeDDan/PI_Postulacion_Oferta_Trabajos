using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{

    [Authorize]
    public class UsuarioIdiomaController : Controller
    {
        private readonly PO_TrabajosContext _context;

        public UsuarioIdiomaController(PO_TrabajosContext context)
        {
            _context = context;
        }
        // GET: IdiomaController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarIdioma([Bind("UsuarioId,IDI_IDIOMA,IDI_ESCRITO,IDI_ORAL")] UsuarioIdioma usuarioIdioma)
        {
            if (ModelState.IsValid)
            {
                _context.UsuarioIdiomas.Add(usuarioIdioma);
                await _context.SaveChangesAsync();

                // Devuelve un resultado JSON si la solicitud es AJAX
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpPost]
        public async Task<IActionResult> EditarIdioma([Bind("IDI_ID,UsuarioId,IDI_IDIOMA,IDI_ESCRITO,IDI_ORAL")] UsuarioIdioma usuarioIdioma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.UsuarioIdiomas.Update(usuarioIdioma);
                    await _context.SaveChangesAsync();

                    // Devuelve un resultado JSON si la solicitud es AJAX
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.UsuarioIdiomas.Any(e => e.IDI_ID == usuarioIdioma.IDI_ID))
                    {
                        return Json(new { success = false, message = "Idioma no encontrado" });
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json(new { success = false, message = "Datos inválidos" });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarIdioma(int id)
        {
            // Verifica si el ID proporcionado es válido
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID inválido" });
            }

            try
            {
                // Busca el idioma en la base de datos
                var idioma = await _context.UsuarioIdiomas.FindAsync(id);
                if (idioma == null)
                {
                    return Json(new { success = false, message = "Idioma no encontrado" });
                }

                // Elimina el idioma
                _context.UsuarioIdiomas.Remove(idioma);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(new { success = false, message = "Error al eliminar el idioma: " + ex.Message });
            }
        }


    }
}
