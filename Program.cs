using System.Text;
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

// Expone únicamente las páginas públicas que queremos que los buscadores indexen.
const string publicSiteUrl = "https://martinruiz.online";
var indexablePaths = new[]
{
    "/",
    "/sobre-mi",
    "/biodescodificacion",
    "/servicios",
    "/mensajes",
    "/pausa-y-papel",
    "/kit-del-giro",
    "/contacto"
};

app.MapGet("/sitemap.xml", () =>
{
    var urls = string.Join(
        Environment.NewLine,
        indexablePaths.Select(path => $"  <url><loc>{publicSiteUrl}{path}</loc></url>"));

    var sitemap =
        $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{Environment.NewLine}" +
        $"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">{Environment.NewLine}" +
        $"{urls}{Environment.NewLine}</urlset>";

    return Results.Text(sitemap, "application/xml", Encoding.UTF8);
});

app.MapGet("/robots.txt", () =>
    Results.Text(
        $"User-agent: *{Environment.NewLine}Allow: /{Environment.NewLine}{Environment.NewLine}" +
        $"Sitemap: {publicSiteUrl}/sitemap.xml{Environment.NewLine}",
        "text/plain",
        Encoding.UTF8));

// Conserva el patrón MVC convencional para las páginas del sitio.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
