# GIRO — Informe de entrega

## Alcance terminado

- Portada de alto impacto con imagen optimizada, identidad GIRO y llamadas a la acción.
- Páginas: Inicio, Sobre mí, Biodescodificación, Servicios, Mensajes, Pausa & Papel, Kit del Giro, Contacto y Privacidad.
- Diseño responsive para escritorio, tablet y celular.
- Navegación móvil, estados de foco y reducción de animaciones según las preferencias del visitante.
- Logo, favicon y metadatos para compartir el sitio en buscadores y redes sociales.
- PDF **El Kit del Giro** disponible para descarga.
- Formulario de contacto validado que prepara la consulta y redirige a WhatsApp.
- Suscripción a mensajes que prepara la solicitud por WhatsApp sin almacenar el correo en el sitio.
- Asistente virtual GIRO con seis preguntas predeterminadas, reconocimiento de consultas escritas, enlaces de acción y aviso de alcance profesional.

## Textos confirmados por Martín

En el trabajo Transgeneracional se muestran los tres movimientos solicitados:

1. **Recordar:** Rastrearemos en tu árbol el origen del patrón que hoy limita tu vida.
2. **Revivir:** Conectamos de forma vivencial con el pasado y descargamos la emoción reprimida.
3. **Reprogramar:** Transmutamos la memoria en coherencia con quién quieres ser a partir de hoy.

El asistente GIRO incluye los valores, modalidad online y respuestas facilitadas por Martín.

## Arquitectura

- ASP.NET Core MVC sobre .NET 9.
- Razor para las vistas.
- CSS propio para todo el sistema visual.
- JavaScript sin dependencias adicionales para menú, suscripción y el asistente GIRO.
- Servicio C# local para las respuestas del asistente.
- Sin base de datos ni claves privadas.

## Verificación realizada

- Sintaxis JavaScript validada.
- CSS con estructura y llaves balanceadas.
- Archivos JSON de configuración validados.
- Enlaces MVC y recursos locales revisados.
- PDF comprobado: 8 páginas A4, sin cifrado ni JavaScript embebido.
- Imágenes convertidas a WebP para reducir el peso de carga.

El entorno donde se preparó la entrega no dispone del SDK de .NET, por lo que la compilación final debe ejecutarse en la computadora de desarrollo con:

```bash
dotnet restore
dotnet build
dotnet run
```

## Antes de publicar definitivamente

Solo restan decisiones de infraestructura: dominio, hosting y, si Martín lo desea, una plataforma de email marketing. El asistente GIRO funciona actualmente con respuestas controladas; una integración futura con IA puede agregarse sin cambiar su interfaz.
