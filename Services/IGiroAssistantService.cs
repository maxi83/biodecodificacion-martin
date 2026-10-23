// Importamos los modelos utilizados
// para construir la respuesta del asistente.
using _14__sisterma_biocodificacion.Models;

namespace _14__sisterma_biocodificacion.Services;

// Esta interfaz establece el contrato del asistente.
// Define qué debe hacer el servicio, pero no explica todavía cómo lo hará.
public interface IGiroAssistantService
{
    // Recibe el mensaje escrito por el visitante
    // y devuelve una respuesta con posibles botones.
    AssistantResponse GetResponse(string message);
}