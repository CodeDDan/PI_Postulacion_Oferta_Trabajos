// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PI_Postulacion_Oferta_Trabajos.Models;
using Microsoft.Extensions.Options;

namespace PI_Postulacion_Oferta_Trabajos.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<Usuario> _userManager;
        private readonly IOptions<IdentityOptions> _identityOptions;

        public LoginModel(SignInManager<Usuario> signInManager, ILogger<LoginModel> logger, UserManager<Usuario> userManager, IOptions<IdentityOptions> identityOptions)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _identityOptions = identityOptions;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Ingrese su correo electrónico.")]
            [EmailAddress(ErrorMessage = "Dirección de correo no válida.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Ingrese su contraseña.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Recordar sesión")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Intento de ingreso no válido.");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    await _userManager.ResetAccessFailedCountAsync(user);

                    // Verificar roles y redirigir
                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        return LocalRedirect("~/Usuario/Index");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "trabajador"))
                    {
                        return LocalRedirect("~/Trabajador/Index");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "empleador"))
                    {
                        return LocalRedirect("~/Ofertas/Index");
                    }

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("La cuenta del usuario está bloqueada.");

                    // Obtener el tiempo de expiración del bloqueo
                    var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                    if (lockoutEnd.HasValue)
                    {
                        var timeRemaining = lockoutEnd.Value - DateTimeOffset.UtcNow;
                        ModelState.AddModelError(string.Empty, $"Tu cuenta está bloqueada. Intenta nuevamente en {timeRemaining.Minutes} minutos y {timeRemaining.Seconds} segundos.");
                    }

                    return Page();
                }
                else
                {
                    var failedAttempts = await _userManager.GetAccessFailedCountAsync(user);
                    var maxAttempts = _identityOptions.Value.Lockout.MaxFailedAccessAttempts; // El número máximo de intentos fallidos antes de bloquear
                    var attemptsLeft = maxAttempts - failedAttempts;
                    if (failedAttempts >= maxAttempts)
                    {
                        ModelState.AddModelError(string.Empty, "Intento de ingreso inválido");
                        ModelState.AddModelError(string.Empty, "Tu cuenta ha sido bloqueada debido a demasiados intentos fallidos.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Intento de inicio de sesión inválido. Te quedan {attemptsLeft} intentos antes de que tu cuenta sea bloqueada.");
                    }
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
