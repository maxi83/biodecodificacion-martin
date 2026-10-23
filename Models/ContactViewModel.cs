// Importamos las validaciones que proporciona .NET.
// Nos permiten indicar qué campos son obligatorios,
// controlar longitudes y validar correos o teléfonos.
using System.ComponentModel.DataAnnotations;

namespace _14__sisterma_biocodificacion.Models;

// Este modelo representa los datos del formulario de contacto.
// La vista enviará estos valores al HomeController.
public class ContactViewModel
{
    // Required indica que el visitante debe completar el campo.
    [Required(ErrorMessage = "Escribí tu nombre.")]

    // Evita que se ingresen textos demasiado largos.
    [StringLength(80)]

    // Define el nombre que aparecerá en el formulario.
    [Display(Name = "Nombre y apellido")]
    public string Nombre { get; set; } = string.Empty;


    [Required(ErrorMessage = "Escribí un número de WhatsApp.")]
    [Phone(ErrorMessage = "Ingresá un número válido.")]
    [StringLength(30)]
    [Display(Name = "WhatsApp")]
    public string WhatsApp { get; set; } = string.Empty;


    // El correo no es obligatorio, por eso utilizamos string?.
    [EmailAddress(ErrorMessage = "Ingresá un correo válido.")]
    [StringLength(120)]
    [Display(Name = "Correo electrónico")]
    public string? Correo { get; set; }


    [Required(ErrorMessage = "Contanos brevemente qué te gustaría conversar.")]

    // El mensaje debe contener entre 10 y 1200 caracteres.
    [StringLength(
        1200,
        MinimumLength = 10,
        ErrorMessage = "Escribí entre 10 y 1200 caracteres."
    )]
    [Display(Name = "¿Qué te gustaría conversar?")]
    public string Mensaje { get; set; } = string.Empty;


    // Le asignamos Online como valor inicial.
    [StringLength(30)]
    [Display(Name = "Modalidad preferida")]
    public string Modalidad { get; set; } = "Online";
}