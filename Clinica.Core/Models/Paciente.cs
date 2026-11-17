using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Core.Models
{
    // [S] Single Responsibility Principle (SRP):
    // Esta entidad tiene una única responsabilidad: representar los datos del paciente.
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
