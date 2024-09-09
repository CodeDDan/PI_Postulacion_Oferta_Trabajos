using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PO_TrabajosContext>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

            // Check if there are any users in the database
            if (await userManager.Users.AnyAsync())
            {
                // Users already exist, no need to seed
                return;
            }

            string[] roles = { "admin", "trabajador", "empleador" };

            // Create roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default user
            var defaultUser = new Usuario
            {
                UsuNombre = "Admin",
                UsuApellido = "Admin",
                Email = "admin@correo.com",
                NormalizedEmail = "ADMIN@CORREO.COM",
                UserName = "admin@correo.com",
                NormalizedUserName = "ADMIN.ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UsuCedula = "1234567890" // Example cedula
            };

            var existingUser = await userManager.FindByNameAsync(defaultUser.UserName);
            if (existingUser == null)
            {
                var result = await userManager.CreateAsync(defaultUser, "SecretPassword123!");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
                if (result.Succeeded)
                {
                    // Assign only the "admin" role
                    await userManager.AddToRoleAsync(defaultUser, "admin");
                }
            }
        }
    }
}
