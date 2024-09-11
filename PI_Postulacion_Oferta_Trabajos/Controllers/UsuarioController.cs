using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller
    {
        private readonly PO_TrabajosContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(PO_TrabajosContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HV()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            // Obtener el listado de UsuarioIdioma para el usuario actual
            var usuarioIdiomas = await _context.UsuarioIdiomas
                .Where(ui => ui.UsuarioId == userId)
                .ToListAsync();

            return View(usuarioIdiomas); // Pasar la lista a la vista
        }
        public async Task<IActionResult> vista_perfil()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;

            // Obtener el listado de UsuarioIdioma para el usuario actual
            var usuarioIdiomas = await _context.UsuarioIdiomas
                .Where(ui => ui.UsuarioId == userId)
                .ToListAsync();

            return View(usuarioIdiomas); // Pasar la lista a la vista
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string usuarioId, string oldPassword, string newPassword, string confirmNewPassword)
        {
            // Validar que la nueva contraseña y la confirmación coincidan
            if (newPassword != confirmNewPassword)
            {
                return Json(new { success = false, message = "Las contraseñas nuevas no coinciden." });
            }

            // Validar que la nueva contraseña cumpla con los requisitos de seguridad
            if (!IsValidPassword(newPassword))
            {
                return Json(new { success = false, message = "La nueva contraseña debe tener al menos 6 caracteres, una letra mayúscula, una letra minúscula, un número y un carácter especial." });
            }

            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                return Json(new { success = false, message = "Usuario no encontrado." });
            }

            // Cambiar la contraseña
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return Json(new { success = true, message = "Contraseña cambiada exitosamente." });
            }

            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        // Método para validar la nueva contraseña
        private bool IsValidPassword(string password)
        {
            // Expresión regular para validar la contraseña
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

    }
}
