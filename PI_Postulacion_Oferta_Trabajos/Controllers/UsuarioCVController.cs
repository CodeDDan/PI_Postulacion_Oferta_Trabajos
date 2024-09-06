using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class UsuarioCVController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly PO_TrabajosContext _context;
        public UsuarioCVController(PO_TrabajosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UploadPdf(IFormFile pdfFile, string UsuarioId)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                // Obtener la fecha y hora actual
                var fechaActual = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Definir el nombre del archivo con el formato UsuarioId_fechaHora.pdf
                var fileName = $"{fechaActual}.pdf";

                // Definir el path donde se guardará el archivo
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CV", fileName);

                // Guardar el archivo en el servidor
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    pdfFile.CopyTo(stream);
                }

                // Crear una nueva instancia de la entidad y asignar los valores
                var nuevoUsuarioCV = new CV
                {
                    UsuarioId = UsuarioId,
                    UCV_NOMBRE = fileName
                };

                // Agregar la nueva instancia al contexto
                _context.CV.Add(nuevoUsuarioCV);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Error al subir el archivo" });
        }
        [HttpGet]
        public IActionResult CheckUserCV(string UsuarioId)
        {
            var usuarioCV = _context.CV.FirstOrDefault(u => u.UsuarioId == UsuarioId);
            if (usuarioCV != null)
            {
                return Json(new { hasCV = true, fileName = usuarioCV.UCV_NOMBRE });
            }
            return Json(new { hasCV = false });
        }
        [HttpPost]
        public IActionResult DeleteCV(string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Json(new { success = false, message = "ID de usuario no proporcionado" });
            }

            // Buscar el CV del usuario en la base de datos
            var usuarioCV = _context.CV.FirstOrDefault(c => c.UsuarioId == usuarioId);
            if (usuarioCV != null)
            {
                // Eliminar el archivo del servidor
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CV", usuarioCV.UCV_NOMBRE);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Eliminar el registro de la base de datos
                _context.CV.Remove(usuarioCV);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "CV no encontrado" });
        }


    }
}
