namespace Clinica.API.DTOs
{
    public class PacienteReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
    }
}
