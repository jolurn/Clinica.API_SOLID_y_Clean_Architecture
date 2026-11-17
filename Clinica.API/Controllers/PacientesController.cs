using Clinica.API.DTOs;
using Clinica.Core.Interfaces;
using Clinica.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Clinica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly INotificacionService _notificacion;

        // [D] Dependency Inversion Principle (DIP):
        // El controlador depende de las abstracciones (interfaces), no de EF Core ni de Email.
        public PacientesController(IPacienteRepository repository, INotificacionService notificacion)
        {
            _repository = repository;
            _notificacion = notificacion;
        }

        // [S] Single Responsibility Principle (SRP):
        // El controlador solo maneja HTTP. No sabe de SQL.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pacientes = await _repository.ObtenerTodosAsync();
            var dtos = pacientes.Select(p => new PacienteReadDTO { Id = p.Id, Nombre = p.Nombre, Dni = p.Dni }).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PacienteCreateDTO dto)
        {
            try
            {
                var paciente = new Paciente { Nombre = dto.Nombre, Dni = dto.Dni };
                await _repository.CrearAsync(paciente);

                // [O] Open/Closed Principle (OCP):
                // El controlador está CERRADO a modificación. Si mañana quieren enviar SMS,
                // NO tocamos este código, solo cambiamos la inyección en Program.cs.
                _notificacion.EnviarBienvenida(paciente);

                return Ok(new { mensaje = "Paciente creado", id = paciente.Id });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key"))
                    return BadRequest(new { mensaje = "El DNI ya se encuentra registrado." });

                return StatusCode(500, new { mensaje = "Error interno en el servidor." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exito = await _repository.EliminarLogicoAsync(id);
            if (!exito) return NotFound();
            return Ok(new { mensaje = "Paciente desactivado (Soft Delete)." });
        }
    }
}