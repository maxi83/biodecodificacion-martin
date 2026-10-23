using System.Collections.Generic;

namespace _14__sisterma_biocodificacion.Models;

// Representa un botón que aparecerá debajo
// de una respuesta del asistente.
public record AssistantAction(
    string Label, // Texto visible del botón.
    string Url    // Dirección que abrirá el botón.
);


// Representa la respuesta completa enviada
// desde el servidor hacia JavaScript.
public record AssistantResponse(
    string Message,                       // Respuesta del asistente.
    IReadOnlyList<AssistantAction> Actions // Botones opcionales.
);