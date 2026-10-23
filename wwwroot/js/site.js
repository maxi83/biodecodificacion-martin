document.addEventListener("DOMContentLoaded", () => {
  initializeMobileNavigation();
  initializeNewsletterForms();
  initializeAssistant();
});

// Abre, cierra y mantiene accesible la navegación en pantallas pequeñas.
function initializeMobileNavigation() {
  const button = document.querySelector("[data-menu-button]");
  const navigation = document.querySelector("[data-main-nav]");

  if (!button || !navigation) return;

  const closeMenu = () => {
    button.classList.remove("is-open");
    navigation.classList.remove("is-open");
    button.setAttribute("aria-expanded", "false");
  };

  button.addEventListener("click", () => {
    const isOpen = navigation.classList.toggle("is-open");
    button.classList.toggle("is-open", isOpen);
    button.setAttribute("aria-expanded", String(isOpen));
  });

  navigation.querySelectorAll("a").forEach(link => link.addEventListener("click", closeMenu));

  document.addEventListener("click", event => {
    if (!navigation.contains(event.target) && !button.contains(event.target)) closeMenu();
  });
}

// Prepara la solicitud de suscripción y la abre en WhatsApp sin guardar datos en el sitio.
function initializeNewsletterForms() {
  document.querySelectorAll("[data-newsletter-form]").forEach(form => {
    form.addEventListener("submit", event => {
      event.preventDefault();
      const email = form.querySelector("input[type='email']");
      const feedback = form.querySelector("[data-form-feedback]");

      if (!email?.checkValidity()) {
        email?.reportValidity();
        return;
      }

      const message = encodeURIComponent(
        `Hola Martín, quiero recibir los mensajes de GIRO. Mi correo es: ${email.value.trim()}`
      );

      if (feedback) feedback.textContent = "Perfecto. Estamos abriendo WhatsApp para completar tu solicitud.";
      window.open(`https://wa.me/543512233519?text=${message}`, "_blank", "noopener,noreferrer");
      form.reset();
    });
  });
}

// Conecta la ventana de GIRO con el endpoint API y mantiene los mensajes como texto seguro.
function initializeAssistant() {
  const launcher = document.querySelector("[data-assistant-launcher]");
  const panel = document.querySelector("[data-assistant-panel]");
  const closeButton = document.querySelector("[data-assistant-close]");
  const form = document.querySelector("[data-assistant-form]");
  const input = document.querySelector("[data-assistant-input]");
  const messages = document.querySelector("[data-assistant-messages]");
  const suggestions = document.querySelector("[data-assistant-suggestions]");

  if (!launcher || !panel || !closeButton || !form || !input || !messages) return;

  const openAssistant = () => {
    panel.classList.add("is-open");
    panel.setAttribute("aria-hidden", "false");
    launcher.classList.add("is-hidden");
    launcher.setAttribute("aria-expanded", "true");
    window.setTimeout(() => input.focus(), 180);
  };

  const closeAssistant = () => {
    panel.classList.remove("is-open");
    panel.setAttribute("aria-hidden", "true");
    launcher.classList.remove("is-hidden");
    launcher.setAttribute("aria-expanded", "false");
    launcher.focus();
  };

  launcher.addEventListener("click", openAssistant);
  closeButton.addEventListener("click", closeAssistant);
  document.addEventListener("keydown", event => {
    if (event.key === "Escape" && panel.classList.contains("is-open")) closeAssistant();
  });

  suggestions?.querySelectorAll("[data-question]").forEach(button => {
    button.addEventListener("click", () => sendMessage(button.dataset.question ?? ""));
  });

  form.addEventListener("submit", event => {
    event.preventDefault();
    sendMessage(input.value);
  });

  async function sendMessage(rawMessage) {
    const message = rawMessage.trim();
    if (!message) return;

    appendMessage(message, "user");
    input.value = "";
    setBusy(true);
    const typing = appendTyping();

    try {
      const response = await fetch("/api/asistente/responder", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ message })
      });

      if (!response.ok) throw new Error("No se pudo obtener una respuesta.");

      const data = await response.json();
      typing.remove();
      const assistantMessage = typeof data.message === "string"
        ? data.message
        : "No recibí una respuesta legible. Podés escribirle directamente a Martín.";
      appendMessage(assistantMessage, "assistant", Array.isArray(data.actions) ? data.actions : []);
    } catch {
      typing.remove();
      appendMessage(
        "No pude responder en este momento. Podés escribirle directamente a Martín por WhatsApp.",
        "assistant",
        [{ label: "Abrir WhatsApp", url: "https://wa.me/543512233519" }]
      );
    } finally {
      setBusy(false);
      input.focus();
    }
  }

  function appendMessage(text, sender, actions = []) {
    const row = document.createElement("div");
    row.className = "chat-row " + (sender === "user" ? "user-row" : "assistant-row");

    if (sender === "assistant") {
      const avatar = document.createElement("span");
      avatar.className = "chat-avatar";
      avatar.setAttribute("aria-hidden", "true");
      avatar.textContent = "G";
      row.appendChild(avatar);
    }

    const bubble = document.createElement("div");
    bubble.className = "chat-bubble";
    bubble.textContent = String(text ?? "");
    row.appendChild(bubble);
    messages.appendChild(row);

    if (actions.length) {
      const actionBar = document.createElement("div");
      actionBar.className = "chat-actions";

      actions.forEach(action => {
        const link = document.createElement("a");
        link.href = action.url;
        link.textContent = action.label;

        if (action.url.startsWith("http")) {
          link.target = "_blank";
          link.rel = "noopener";
        }

        actionBar.appendChild(link);
      });

      messages.appendChild(actionBar);
    }

    scrollMessages();
  }

  function appendTyping() {
    const row = document.createElement("div");
    row.className = "chat-row assistant-row chat-typing";
    const avatar = document.createElement("span");
    avatar.className = "chat-avatar";
    avatar.textContent = "G";
    const bubble = document.createElement("div");
    bubble.className = "chat-bubble";
    bubble.textContent = "GIRO está escribiendo…";
    row.append(avatar, bubble);
    messages.appendChild(row);
    scrollMessages();
    return row;
  }

  function setBusy(isBusy) {
    input.disabled = isBusy;
    form.querySelector("button[type='submit']").disabled = isBusy;
    suggestions?.querySelectorAll("button").forEach(button => button.disabled = isBusy);
  }

  function scrollMessages() {
    messages.scrollTop = messages.scrollHeight;
  }
}
