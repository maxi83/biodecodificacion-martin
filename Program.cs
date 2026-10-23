using _14__sisterma_biocodificacion.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra MVC para habilitar controladores, vistas y enlace de modelos.
builder.Services.AddControllersWithViews();
// Registra el servicio del asistente para que pueda inyectarse en el controlador API.
builder.Services.AddSingleton<IGiroAssistantService, GiroAssistantService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
// Permite servir los recursos ubicados en wwwroot, como CSS, JavaScript e imágenes.
app.UseStaticFiles();
app.UseRouting();
// Activa la autorización para las rutas que la requieran.
app.UseAuthorization();

// Habilita los endpoints de controladores que usan atributos de ruta.
app.MapStaticAssets();
app.MapControllers();

// Conserva el patrón MVC convencional para las páginas del sitio.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
