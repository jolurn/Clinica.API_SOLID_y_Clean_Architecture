using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Infrastructure.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Integridad de datos: Índice único para no duplicar DNI.
            modelBuilder.Entity<Paciente>().HasIndex(p => p.Dni).IsUnique();

            // Soft Delete Global: Oculta los inactivos automáticamente.
            modelBuilder.Entity<Paciente>().HasQueryFilter(p => p.Activo);
        }
    }
}
