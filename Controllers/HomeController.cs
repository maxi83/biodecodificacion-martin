using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _14__sisterma_biocodificacion.Models;
using GiroMartinRuiz.Models;

namespace GiroMartinRuiz.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index() => View();

    [HttpGet("/sobre-mi")]
    public IActionResult SobreMi() => View();

    [HttpGet("/biodescodificacion")]
    public IActionResult Biodescodificacion() => View();

    [HttpGet("/servicios")]
    public IActionResult Servicios() => View();

    [HttpGet("/mensajes")]
    public IActionResult Mensajes() => View();

    [HttpGet("/pausa-y-papel")]
    public IActionResult PausaPapel() => View();

    [HttpGet("/kit-del-giro")]
    public IActionResult Kit() => View();

    [HttpGet("/contacto")]
    // Entrega un modelo vacío para que la vista pueda mostrar el formulario.
    public IActionResult Contacto() => View(new ContactViewModel());

    [HttpPost("/contacto")]
    [ValidateAntiForgeryToken]
    // Valida el formulario antes de preparar la comunicación con Martín.
    public IActionResult Contacto(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _logger.LogInformation("Nueva consulta web validada. Modalidad: {Modalidad}", model.Modalidad);

        var whatsappMessage = Uri.EscapeDataString(
            $"Hola Martín, soy {model.Nombre}. " +
            $"Tipo de consulta: {model.Modalidad}. " +
            $"Mi consulta: {model.Mensaje} " +
            $"WhatsApp: {model.WhatsApp}. " +
            $"Correo: {model.Correo ?? "No informado"}.");

        return Redirect($"https://wa.me/543512233519?text={whatsappMessage}");
    }

    [HttpGet("/privacidad")]
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        ViewData["StatusCode"] = statusCode;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
