using System.Globalization;
using System.Text;
using _14__sisterma_biocodificacion.Models;

namespace _14__sisterma_biocodificacion.Services;

// Esta clase contiene la lógica y las respuestas
// del asistente virtual de GIRO.
public class GiroAssistantService : IGiroAssistantService
{
    // Guardamos el enlace de WhatsApp en un solo lugar.
    // Si Martín cambia de número, solamente modificamos esta línea.
    private const string WhatsAppUrl =
        "https://wa.me/543512233519";


    // Este método recibe la pregunta del visitante.
    public AssistantResponse GetResponse(string message)
    {
        // Convertimos el mensaje a minúsculas y eliminamos tildes.
        // Así podemos reconocer “sesión”, “Sesion” o “SESIÓN”.
        string normalizedMessage = Normalize(message);


        // ---------------------------------------------------------
        // PREGUNTAS FRECUENTES
        // ---------------------------------------------------------

        if (ContainsAny(normalizedMessage, "diferencia entre los 4", "diferencia entre 4")
            || (ContainsAny(normalizedMessage, "4 servicios", "cuatro servicios")
                && ContainsAny(normalizedMessage, "necesito", "cual elegir")))
        {
            return CreateResponse(
                "Biodescodificación: Para destrabar un síntoma físico o conflicto puntual de tu presente.\n\nTransgeneracional: Para sanar patrones, culpas o lealtades repetitivas que heredaste de tu árbol familiar.\n\nProyecto Sentido: Para comprender los bloqueos nacidos en tu gestación o infancia temprana.\n\nPausa y Papel: Tu kit de emergencia autogestionable (4 días) para vaciar la rumiación y el estrés mental.");
        }

        if (ContainsAny(normalizedMessage, "problema es propio", "viene de mi arbol", "viene del arbol")
            || (ContainsAny(normalizedMessage, "transgeneracional", "ancestro")
                && ContainsAny(normalizedMessage, "problema", "propio", "arbol")))
        {
            return CreateResponse(
                "No hace falta que lo sepas antes. En la primera consulta evaluamos tu motivo actual; si detectamos que estás repitiendo la historia de un ancestro o cargando un mandato ajeno, pasamos al trabajo transgeneracional.");
        }

        if (ContainsAny(normalizedMessage, "que se trabaja", "que trabajamos")
            && ContainsAny(normalizedMessage, "proyecto sentido"))
        {
            return CreateResponse(
                "Exploramos las emociones, deseos y vivencias de tus padres desde 9 meses antes de tu concepción hasta tus primeros años de vida. Sanar esta etapa te libera de expectativas o mandatos inconscientes con los que fuiste concebido.");
        }

        if ((ContainsAny(normalizedMessage, "cuantas", "cuantas sesiones", "terapia de anos")
                && ContainsAny(normalizedMessage, "sesion", "cambios", "necesito"))
            || ContainsAny(normalizedMessage, "frecuencia semanal"))
        {
            return CreateResponse(
                "No es una terapia de años ni de frecuencia semanal fija. Al ser un enfoque directo a la causa raíz del conflicto, la gran mayoría de las personas logran comprensiones profundas y alivio desde la primera sesión.");
        }

        if ((ContainsAny(normalizedMessage, "valor tienen tus servicios", "valor de tus servicios")
                || (ContainsAny(normalizedMessage, "valor", "precio", "cuanto")
                    && ContainsAny(normalizedMessage, "pausa", "papel", "servicios")))
            || (ContainsAny(normalizedMessage, "incluye", "que incluye")
                && ContainsAny(normalizedMessage, "pausa", "papel")))
        {
            return CreateResponse(
                "Las sesiones individuales (90 minutos por sesión) están en $66.000 ARS. Para quienes no vivan en Argentina, el valor es de 55 dólares.\n\nPausa & Papel es un protocolo autogestionable con valor de lanzamiento de $27.000 ARS (para los primeros 100 cupos) que incluye 7 microvideos, guía en PDF y acceso por un año. Para el exterior (toda persona que no resida en Argentina), el valor es de 20 dólares.");
        }

        if ((ContainsAny(normalizedMessage, "como son las sesiones", "sesiones individuales")
                && ContainsAny(normalizedMessage, "online", "videollamada", "tomarlas", "donde"))
            || ContainsAny(normalizedMessage, "desde donde puedo"))
        {
            return CreateResponse(
                "Son 100% online por videollamada. Solo necesitás un espacio privado sin interrupciones y buena conexión a internet, sin importar en qué parte del mundo estés.");
        }


        // ---------------------------------------------------------
        // SEGURIDAD Y EMERGENCIAS
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "urgencia",
            "emergencia",
            "suicidio",
            "lastimar",
            "hacerme dano"))
        {
            return CreateResponse(
                "Si estás atravesando una urgencia o sentís que podés lastimarte, buscá ayuda inmediata. Llamá al servicio de emergencias de tu zona o acercate a una guardia. Este asistente no puede atender emergencias.",
                new AssistantAction(
                    "Hablar con Martín",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // EL KIT DEL GIRO
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "kit",
            "guia",
            "pdf",
            "descargar",
            "regalo"))
        {
            return CreateResponse(
                "El Kit del Giro es una guía gratuita con cinco pasos para empezar a observar emociones y patrones cotidianos con mayor claridad.",
                new AssistantAction(
                    "Descargar el Kit",
                    "/docs/Kit-del-Giro.pdf"
                ),
                new AssistantAction(
                    "Ver los cinco pasos",
                    "/kit-del-giro"
                )
            );
        }


        // ---------------------------------------------------------
        // PAUSA Y PAPEL
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "pausa",
            "papel",
            "escritura",
            "escribir"))
        {
            return CreateResponse(
                "Pausa & Papel es un protocolo de escritura consciente para ordenar ideas, expresar lo que sentís y ganar claridad a tu propio ritmo. Tus escritos son privados y quedan con vos.",
                new AssistantAction(
                    "Conocer el protocolo",
                    "/pausa-y-papel"
                ),
                new AssistantAction(
                    "Hablar con Martín",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // TRANSGENERACIONAL
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "transgeneracional",
            "arbol",
            "ancestro",
            "familia"))
        {
            return CreateResponse(
                "El trabajo transgeneracional propone observar historias y patrones familiares que podrían estar influyendo en tu presente. El proceso se trabaja mediante tres movimientos: recordar, revivir y reprogramar.",
                new AssistantAction(
                    "Ver servicios",
                    "/servicios"
                ),
                new AssistantAction(
                    "Consultar por WhatsApp",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // PROYECTO SENTIDO
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "proyecto sentido",
            "profesion",
            "trabajo",
            "mandato"))
        {
            return CreateResponse(
                "Proyecto Sentido permite explorar necesidades, expectativas y mandatos familiares que pudieron quedar registrados durante las primeras etapas de tu vida.",
                new AssistantAction(
                    "Ver Proyecto Sentido",
                    "/servicios"
                ),
                new AssistantAction(
                    "Hablar con Martín",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // BIODESCODIFICACIÓN
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "biodescodificacion",
            "biodecodificacion",
            "que es",
            "como funciona"))
        {
            return CreateResponse(
                "La Biodescodificación se presenta en GIRO como un espacio complementario de reflexión para observar emociones, historias y patrones repetitivos. No diagnostica ni reemplaza la atención médica, psicológica o psiquiátrica.",
                new AssistantAction(
                    "Conocer el enfoque",
                    "/biodescodificacion"
                ),
                new AssistantAction(
                    "Ver servicios",
                    "/servicios"
                )
            );
        }


        // ---------------------------------------------------------
        // TURNOS Y MODALIDADES
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "online",
            "presencial",
            "modalidad",
            "horario",
            "turno",
            "reservar",
            "agendar"))
        {
            return CreateResponse(
                "Las sesiones individuales son 100% online. Podés conversar directamente con Martín para coordinar un horario.",
                new AssistantAction(
                    "Abrir WhatsApp",
                    WhatsAppUrl
                ),
                new AssistantAction(
                    "Ir a contacto",
                    "/contacto"
                )
            );
        }


        // ---------------------------------------------------------
        // SERVICIOS Y SESIONES
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "sesion",
            "servicio",
            "elegir",
            "acompanamiento"))
        {
            return CreateResponse(
                "GIRO ofrece sesiones individuales de Biodescodificación, Transgeneracional, Proyecto Sentido y Pausa & Papel. Puedo ayudarte a conocer cada propuesta.",
                new AssistantAction(
                    "Ver servicios",
                    "/servicios"
                ),
                new AssistantAction(
                    "Hablar con Martín",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // PRECIOS
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "precio",
            "valor",
            "cuanto",
            "costo"))
        {
            return CreateResponse(
                "Las sesiones individuales duran 90 minutos y su valor actual es de $66.000 ARS o 55 dólares para quienes no residan en Argentina. Pausa & Papel tiene un valor de lanzamiento de $27.000 ARS para los primeros 100 cupos, o 20 dólares para el exterior. La disponibilidad se confirma directamente con Martín.",
                new AssistantAction(
                    "Consultar por WhatsApp",
                    WhatsAppUrl
                )
            );
        }


        // ---------------------------------------------------------
        // SALUDOS
        // ---------------------------------------------------------

        if (ContainsAny(
            normalizedMessage,
            "hola",
            "buen dia",
            "buenas",
            "como estas"))
        {
            return CreateResponse(
                "¡Hola! Soy el asistente virtual de GIRO. Puedo contarte sobre los servicios, el Kit gratuito, Pausa & Papel o ayudarte a conversar con Martín.",
                new AssistantAction(
                    "Ver servicios",
                    "/servicios"
                ),
                new AssistantAction(
                    "Descargar el Kit",
                    "/docs/Kit-del-Giro.pdf"
                )
            );
        }


        // ---------------------------------------------------------
        // RESPUESTA GENERAL
        // ---------------------------------------------------------

        // Esta respuesta se utiliza cuando no encontramos
        // ninguna palabra relacionada con las categorías anteriores.
        return CreateResponse(
            "Puedo orientarte sobre los servicios de GIRO, el Kit gratuito y las formas de contacto. Para recibir una respuesta personal, lo mejor es conversar directamente con Martín.",
            new AssistantAction(
                "Ver servicios",
                "/servicios"
            ),
            new AssistantAction(
                "Hablar con Martín",
                WhatsAppUrl
            )
        );
    }


    // Crea el objeto que después será enviado como JSON a JavaScript.
    private static AssistantResponse CreateResponse(
        string message,
        params AssistantAction[] actions)
    {
        return new AssistantResponse(message, actions);
    }


    // Comprueba si el mensaje contiene alguna
    // de las palabras que estamos buscando.
    private static bool ContainsAny(
        string source,
        params string[] values)
    {
        return values.Any(value =>
            source.Contains(
                value,
                StringComparison.Ordinal
            )
        );
    }


    // Convierte el texto a minúsculas y elimina las tildes.
    private static string Normalize(string value)
    {
        string decomposedText = value
            .Trim()
            .ToLowerInvariant()
            .Normalize(NormalizationForm.FormD);

        StringBuilder normalizedText = new();

        foreach (char character in decomposedText)
        {
            UnicodeCategory category =
                CharUnicodeInfo.GetUnicodeCategory(character);

            // Solamente agregamos caracteres que no sean tildes.
            if (category != UnicodeCategory.NonSpacingMark)
            {
                normalizedText.Append(character);
            }
        }

        return normalizedText
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
}
