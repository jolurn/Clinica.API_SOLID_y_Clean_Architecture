using Clinica.Core.Interfaces;
using Clinica.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Infrastructure.Notificaciones
{
    // [L] Liskov Substitution Principle (LSP):
    // Cualquier clase que implemente INotificacionService puede usarse aquí.

    // [S] Single Responsibility Principle (SRP):
    // Su única responsabilidad es enviar correos.
    public class EmailNotificacionService : INotificacionService
    {
        public void EnviarBienvenida(Paciente paciente)
        {
            // Lógica de envío de correo
            Console.WriteLine($"[EMAIL] Bienvenido {paciente.Nombre}");
        }
    }
}
