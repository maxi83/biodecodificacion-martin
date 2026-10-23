# GIRO — Guía rápida para Axel

Este proyecto parte de `dotnet new mvc` y mantiene la separación clásica de ASP.NET Core MVC.

## Cómo ejecutarlo

1. Abrí una terminal dentro de esta carpeta.
2. Ejecutá `dotnet restore`.
3. Ejecutá `dotnet build` para comprobar que todo compile.
4. Ejecutá `dotnet run`.
5. Abrí `http://localhost:5134` o la dirección que muestre la terminal.

## Cómo compartir el avance desde VS Code

1. Dejá el proyecto ejecutándose con `dotnet run`.
2. Abrí el panel **Puertos** de VS Code.
3. Reenviá el puerto `5134`.
4. Cambiá su visibilidad a **Público**.
5. Copiá la dirección reenviada y compartila con Martín.

Mientras la terminal esté ejecutándose y la computadora siga conectada, el enlace podrá mostrar el sitio.

## Orden recomendado para estudiarlo

1. `Program.cs`: registra MVC y el servicio del asistente.
2. `Controllers/HomeController.cs`: conecta las rutas públicas con cada vista.
3. `Views/Shared/_Layout.cshtml`: encabezado, navegación, pie y scripts comunes.
4. `Views/Home/Index.cshtml`: estructura de la portada.
5. Resto de `Views/Home`: una vista por página pública.
6. `wwwroot/css/site.css`: paleta, componentes y adaptación para celular.
7. `Views/Shared/_AssistantWidget.cshtml`: interfaz común del chat.
8. `wwwroot/js/site.js`: menú, formulario de novedades y comunicación del chat.
9. `Controllers/AsistenteController.cs`: endpoint que recibe cada consulta.
10. `Services/GiroAssistantService.cs`: reglas y respuestas del asistente.

## Qué ya funciona

- Nueve páginas públicas responsive, incluida la política de privacidad.
- Navegación de escritorio y celular.
- Datos reales de Martín y botones de WhatsApp.
- Descarga del PDF El Kit del Giro.
- Formulario de contacto con validación del lado del cliente y del servidor.
- Asistente virtual con respuestas sobre servicios, Kit, sesiones, precios y contacto.
- Respuesta especial de seguridad para mensajes que indiquen una urgencia.
- Metadatos para buscadores y redes sociales, favicon propio e imágenes optimizadas.

## Qué necesita integración antes de producción

- El formulario valida los datos y prepara el mensaje en WhatsApp. No guarda consultas en una base de datos.
- La suscripción del inicio prepara una solicitud por WhatsApp sin guardar el correo en el sitio. A futuro puede conectarse con una herramienta de email marketing.
- El asistente usa una base local de respuestas por intención. Puede conectarse después con una API de IA conservando estas reglas de seguridad.

## Regla de aprendizaje

Modificá una sola capa por vez, ejecutá el proyecto y comprobá el resultado antes de continuar. Primero las vistas, después estilos, luego JavaScript y finalmente controladores y servicios.
