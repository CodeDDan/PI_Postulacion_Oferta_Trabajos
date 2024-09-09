// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

namespace PI_Postulacion_Oferta_Trabajos.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly PO_TrabajosContext _context;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUserStore<Usuario> _userStore;
        private readonly IUserEmailStore<Usuario> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            PO_TrabajosContext context,
            UserManager<Usuario> userManager,
            IUserStore<Usuario> userStore,
            SignInManager<Usuario> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            // Aquí definimos los campos para el ingreso de nuestro modelo
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            public string UsuNombre { get; set; }

            [Required(ErrorMessage = "El apellido es obligatorio.")]
            public string UsuApellido { get; set; }

            [Required(ErrorMessage = "La cédula es obligatoria.")]
            public string UsuCedula { get; set; }

            [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
            public string PhoneNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Ingrese la contraseña.")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres y no más de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirar contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            public string ConfirmPassword { get; set; }

            // Campos para el registro de usuario empelador
            // Campos para la validación de empresa
            public string? EmpEmailAcceso { get; set; }
            public string? EmpPassword { get; set; }

            // Nueva propiedad para el switch o check
            [Required]
            [Display(Name = "Crear como usuario empresa")]
            public bool esEmpleador { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new Usuario()
                {
                    UsuNombre = Input.UsuNombre,
                    UsuApellido = Input.UsuApellido,
                    UsuCedula = Input.UsuCedula,
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                };

                bool valor = Input.esEmpleador;

                // Validar el modelo Usuario
                var context = new ValidationContext(user, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(user, context, validationResults, true);

                if (isValid)
                {
                    string rolAsignado = "";

                    // Comprobaremos que rol debemos asignar
                    if (valor)
                    {
                        // Verificar si la empresa existe en la base de datos con el email y la contraseña dados
                        var empresa = await _context.Empresas
                            .FirstOrDefaultAsync(e => e.EmpEmailAcceso == Input.EmpEmailAcceso && e.EmpPassword == Input.EmpPassword);

                        if (empresa == null)
                        {
                            // Si no se encuentra la empresa, se retorna un error
                            ModelState.AddModelError(string.Empty, "El email o la contraseña de la empresa son incorrectos.");
                            return Page();
                        }

                        rolAsignado = "empleador";
                        // Si la empresa es válida, asignamos el ID de la empresa al usuario
                        user.UsuarioIdEmpresa = empresa.EmpId;
                    }
                    else
                    {
                        rolAsignado = "trabajador";
                    }
                    // Creamos el usuario después de las validaciones
                    await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        // Creamos el usuario con el rol
                        await _userManager.AddToRoleAsync(user, rolAsignado);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "DuplicateUserName":
                                ModelState.AddModelError(string.Empty, $"El correo {Input.Email} ya está registrado.");
                                break;
                            case "PasswordTooShort":
                                ModelState.AddModelError(string.Empty, "La contraseña debe tener al menos 6 caracteres.");
                                break;
                            case "PasswordRequiresUpper":
                                ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos una letra mayúscula.");
                                break;
                            case "PasswordRequiresLower":
                                ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos una letra minúscula.");
                                break;
                            case "PasswordRequiresDigit":
                                ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos un dígito.");
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos un carácter especial.");
                                break;
                            case "UserLockedOut":
                                ModelState.AddModelError(string.Empty, "Tu cuenta está bloqueada. Por favor, contacta al administrador.");
                                break;
                            case "UserNotFound":
                                ModelState.AddModelError(string.Empty, "El usuario no se encontró.");
                                break;
                            case "InvalidToken":
                                ModelState.AddModelError(string.Empty, "El token de recuperación de contraseña es inválido.");
                                break;
                            case "InvalidEmail":
                                ModelState.AddModelError(string.Empty, "El formato del correo electrónico no es válido.");
                                break;
                            case "InvalidPassword":
                                ModelState.AddModelError(string.Empty, "La contraseña no es válida.");
                                break;
                            case "EmailAlreadyConfirmed":
                                ModelState.AddModelError(string.Empty, "El correo electrónico ya ha sido confirmado.");
                                break;
                            case "UserAlreadyInRole":
                                ModelState.AddModelError(string.Empty, "El usuario ya tiene el rol especificado.");
                                break;
                            case "RoleNotFound":
                                ModelState.AddModelError(string.Empty, "El rol no se encontró.");
                                break;
                            default:
                                ModelState.AddModelError(string.Empty, error.Description);
                                break;
                        }
                    }
                }
                else
                {
                    // Si la validación falla, agregar los errores al ModelState
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Usuario CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Usuario>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Usuario)}'. " +
                    $"Ensure that '{nameof(Usuario)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Usuario> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Usuario>)_userStore;
        }
    }
}
