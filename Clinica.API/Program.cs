using Clinica.Core.Interfaces;
using Clinica.Infrastructure.Data;
using Clinica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Clinica.Infrastructure.Notificaciones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// [D] Dependency Inversion Principle (DIP) en su máxima expresión:
// Le decimos al contenedor de .NET: "Cuando el Controlador pida IPacienteRepository, 
// entrégale PacienteRepository. Y cuando pida INotificacionService, dale EmailNotificacionService".
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<INotificacionService, EmailNotificacionService>();

var app = builder.Build();

// Middleware de Errores Global
app.UseExceptionHandler(errorApp => {
    errorApp.Run(async context => {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { Mensaje = "Error inesperado en el servidor." });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();