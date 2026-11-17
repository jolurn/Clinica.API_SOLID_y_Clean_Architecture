using Clinica.Core.Interfaces;
using Clinica.Core.Models;
using Clinica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica.Infrastructure.Repositories
{
    // [L] Liskov Substitution Principle (LSP):
    // Podría ser reemplazada por una implementación con Dapper o MongoDB sin romper la API.

    // [S] Single Responsibility Principle (SRP):
    // Su única responsabilidad es la comunicación con SQL Server a través de EF Core.
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ClinicaDbContext _context;
        public PacienteRepository(ClinicaDbContext context) => _context = context;

        public async Task<List<Paciente>> ObtenerTodosAsync() => await _context.Pacientes.AsNoTracking().ToListAsync();

        public async Task<Paciente?> ObtenerPorIdAsync(int id) => await _context.Pacientes.FindAsync(id);

        public async Task<Paciente> CrearAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<bool> EliminarLogicoAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return false;
            paciente.Activo = false;
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
        
}