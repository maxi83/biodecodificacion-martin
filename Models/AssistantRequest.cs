using System.ComponentModel.DataAnnotations;

namespace _14__sisterma_biocodificacion.Models;

// Representa la pregunta que el visitante
// le envía al asistente virtual GIRO.
public class AssistantRequest
{
    // La consulta es obligatoria.
    [Required(ErrorMessage = "Escribí una consulta.")]

    // Limitamos el mensaje para evitar textos excesivamente largos.
    [StringLength(
        500,
        ErrorMessage = "La consulta no puede superar los 500 caracteres."
    )]
    public string Message { get; set; } = string.Empty;
}