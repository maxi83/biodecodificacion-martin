using Microsoft.AspNetCore.Mvc;
using _14__sisterma_biocodificacion.Models;
using _14__sisterma_biocodificacion.Services;

namespace GiroMartinRuiz.Controllers;

// Indica que esta clase recibe y devuelve datos HTTP en formato de API.
[ApiController]
// Todas las rutas de este controlador comienzan con api/asistente.
[Route("api/asistente")]
public class AsistenteController : ControllerBase
{
    private readonly IGiroAssistantService _assistantService;

    // ASP.NET Core obtiene automáticamente el servicio registrado y lo entrega aquí.
    public AsistenteController(IGiroAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    // Atiende las consultas enviadas mediante POST a api/asistente/responder.
    [HttpPost("responder")]
    [ProducesResponseType<AssistantResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AssistantResponse> Responder([FromBody] AssistantRequest request)
    {
        // Validamos tanto las reglas del modelo como el contenido real del mensaje.
        if (!ModelState.IsValid || request is null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new { message = "Escribí una consulta para continuar." });
        }

        // Solo enviamos consultas válidas al servicio que contiene la lógica del asistente.
        return Ok(_assistantService.GetResponse(request.Message));
    }
}
