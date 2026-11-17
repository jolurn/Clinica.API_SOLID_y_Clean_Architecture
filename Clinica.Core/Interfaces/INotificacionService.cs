using Clinica.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Core.Interfaces
{
    // [I] Interface Segregation Principle (ISP):
    // Interfaz segregada solo para notificaciones.
    public interface INotificacionService
    {
        void EnviarBienvenida(Paciente paciente);
    }
}
