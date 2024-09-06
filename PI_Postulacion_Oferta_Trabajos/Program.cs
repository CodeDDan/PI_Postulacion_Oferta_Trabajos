using Microsoft.EntityFrameworkCore;
using PI_Postulacion_Oferta_Trabajos.Models;
using PI_Postulacion_Oferta_Trabajos.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using PI_Postulacion_Oferta_Trabajos.CustomValidations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Hace que el comportamiento predeterminado de ASP.NET Core en cuanto a los tipos de referencia no anulables se suprima.
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


builder.Services.AddDbContext<PO_TrabajosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura la identidad con la pol�tica de bloqueo de cuentas
builder.Services.AddDefaultIdentity<Usuario>(options =>
{
    // Configuraci�n del bloqueo de cuenta
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Tiempo de bloqueo
    options.Lockout.MaxFailedAccessAttempts = 3; // Verificar
    options.Lockout.AllowedForNewUsers = false; // Permitir bloqueo para nuevos usuarios

    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<PO_TrabajosContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Si usamos identity se debe agregar si o si lo siguiente
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
