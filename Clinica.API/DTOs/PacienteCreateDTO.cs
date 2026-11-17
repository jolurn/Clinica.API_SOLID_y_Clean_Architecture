using System.ComponentModel.DataAnnotations;

namespace Clinica.API.DTOs
{
    public class PacienteCreateDTO
    {
        [Required] public string Nombre { get; set; } = string.Empty;
        [Required, StringLength(8)] public string Dni { get; set; } = string.Empty;
    }
}
