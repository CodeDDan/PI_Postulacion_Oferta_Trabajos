using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class UserViewModel
    {
        public Usuario Usuario { get; set; } = new Usuario();  // Asegúrate de que no sea null
        public string Roles { get; set; } = string.Empty;  // Inicializa como cadena vacía
        public string SelectedRole { get; set; } = string.Empty; // Nuevo campo para seleccionar el rol
        public string EmpresaEmail { get; set; } = string.Empty;
        public string EmpresaPassword { get; set; } = string.Empty;
    }
    public class UsuarioController : Controller
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PO_TrabajosContext _context;

        public UsuarioController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, PO_TrabajosContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Usuario
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = users.Select(user => new UserViewModel
            {
                Usuario = user,
                Roles = string.Join(", ", _userManager.GetRolesAsync(user).Result)
            }).ToList();

            return View(userViewModels);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(usuario);

            var viewModel = new UserViewModel
            {
                Usuario = usuario,
                Roles = string.Join(", ", roles)
            };

            return View(viewModel);
        }

        // GET: Usuario/Create
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync(); // Obtener lista de roles
            ViewBag.Roles = new SelectList(roles, "Name", "Name"); // Pasar los roles a la vista
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedRole == "empleador")
                {
                    var empresa = await _context.Empresas
                        .FirstOrDefaultAsync(e => e.EmpEmailAcceso == model.EmpresaEmail && e.EmpPassword == model.EmpresaPassword);

                    if (empresa == null)
                    {
                        ModelState.AddModelError("", "Los datos de la empresa no son válidos.");
                        var rol = await _roleManager.Roles.ToListAsync();
                        ViewBag.Roles = new SelectList(rol, "Name", "Name");
                        return View(model);
                    }

                    model.Usuario.UsuarioIdEmpresa = empresa.EmpId;
                }

                model.Usuario.UserName = model.Usuario.Email; // Asignar el email al UserName

                var result = await _userManager.CreateAsync(model.Usuario, "DefaultPassword123!"); // Contraseña Predeterminada
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(model.Usuario, model.SelectedRole); // Asignar rol al usuario
                    }
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name"); // Asegúrate de pasar los roles de nuevo si hay un error
            return View(model);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(usuario);

            var model = new UserViewModel
            {
                Usuario = usuario,
                Roles = string.Join(", ", userRoles)
            };

            ViewBag.Roles = new SelectList(roles, "Name", "Name", userRoles);

            return View(model);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                if (model.Roles == "empleador")
                {
                    var empresa = await _context.Empresas
                        .FirstOrDefaultAsync(e => e.EmpEmailAcceso == model.EmpresaEmail && e.EmpPassword == model.EmpresaPassword);

                    if (empresa == null)
                    {
                        ModelState.AddModelError("", "Los datos de la empresa no son válidos.");
                        var roles = await _roleManager.Roles.ToListAsync();
                        ViewBag.Roles = new SelectList(roles, "Name", "Name");
                        return View(model);
                    }

                    usuario.UsuarioIdEmpresa = empresa.EmpId;
                }
                else
                {
                    usuario.UsuarioIdEmpresa = null;
                }
                usuario.UsuNombre = model.Usuario.UsuNombre;
                usuario.UsuApellido = model.Usuario.UsuApellido;
                usuario.UsuCedula = model.Usuario.UsuCedula;
                usuario.Email = model.Usuario.Email;
                usuario.UserName = model.Usuario.Email; // Sincronizar UserName con Email

                var result = await _userManager.UpdateAsync(usuario);
                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(usuario);
                    var selectedRoles = model.Roles.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    var rolesToAdd = selectedRoles.Except(currentRoles);
                    var rolesToRemove = currentRoles.Except(selectedRoles);

                    await _userManager.AddToRolesAsync(usuario, rolesToAdd);
                    await _userManager.RemoveFromRolesAsync(usuario, rolesToRemove);

                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var allRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(allRoles, "Name", "Name", model.Roles.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
            return View(model);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(usuario);
            var viewModel = new UserViewModel
            {
                Usuario = usuario,
                Roles = string.Join(", ", roles)
            };

            return View(viewModel);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            var roles = await _userManager.GetRolesAsync(usuario);
            var viewModel = new UserViewModel
            {
                Usuario = usuario,
                Roles = string.Join(", ", roles)
            };

            return View("Delete", viewModel);
        }
    }
}
