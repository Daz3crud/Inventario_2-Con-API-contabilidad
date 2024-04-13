using Inventario_2.Controllers;
using Inventario_2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<InventarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddSingleton<ContabilidadService>();
builder.Services.AddHttpClient<AsientosContablesController>(client => { 
    client.BaseAddress = new Uri("http://ap1-contabilidad.azurewebsites.net/");
});


var app = builder.Build();

// Migración del modelo AsientoContable para excluirlo del contexto
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<InventarioContext>();

  
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Agregar la ruta para el método TestConnection en AsientoContablesController
app.MapControllerRoute(
    name: "TestConnection",
    pattern: "AsientoContables/TestConnection",
    defaults: new { controller = "AsientoContables", action = "TestConnection" }
);

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
