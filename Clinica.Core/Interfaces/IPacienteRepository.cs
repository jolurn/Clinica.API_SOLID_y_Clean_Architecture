using Clinica.Core.Models;

namespace Clinica.Core.Interfaces
{
    // [I] Interface Segregation Principle (ISP):
    // Interfaz segregada solo para las operaciones de la base de datos de Pacientes.
    public interface IPacienteRepository
    {
        Task<List<Paciente>> ObtenerTodosAsync();
        Task<Paciente?> ObtenerPorIdAsync(int id);
        Task<Paciente> CrearAsync(Paciente paciente);
        Task<bool> EliminarLogicoAsync(int id);
    }
        
}