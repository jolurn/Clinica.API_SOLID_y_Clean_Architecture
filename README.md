# 🏥 Clínica API - Clean Architecture & SOLID

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

Una API RESTful desarrollada en **C# y ASP.NET Core** para la gestión de pacientes. Este proyecto implementa **Clean Architecture (Arquitectura Limpia)** con 3 proyectos separados y aplica estrictamente los **5 Principios SOLID** para garantizar un código escalable y mantenible.

## 🏗️ Estructura y Código (Haz clic en los nombres para ver el código)

La solución está dividida en tres proyectos para respetar la regla de dependencia:

*   📁 **Capa Core (El Dominio):** Es independiente de frameworks y bases de datos.
    *   [Entidad Paciente](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Models/Paciente.cs)
    *   [Interfaz IPacienteRepository](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Interfaces/IPacienteRepository.cs)
    *   [Interfaz INotificacionService](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Interfaces/INotificacionService.cs)
*   📁 **Capa Infrastructure:** Implementa las herramientas externas y la base de datos.
    *   [ClinicaDbContext (EF Core + Fluent API)](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Infrastructure/Data/ClinicaDbContext.cs)
    *   [PacienteRepository (Implementación)](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Infrastructure/Repositories/PacienteRepository.cs)
    *   [EmailNotificacionService](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Infrastructure/Notificaciones/EmailNotificacionService.cs)
*   📁 **Capa API (Presentación):** Controladores HTTP y configuración.
    *   [PacientesController](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.API/Controllers/PacientesController.cs)
    *   [Program.cs (Inyección de Dependencias)](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.API/Program.cs)

## 🛠️ Principios SOLID Aplicados (Clic para ver la implementación)

*   **S - Single Responsibility (SRP):** Aplicado en el [PacientesController](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.API/Controllers/PacientesController.cs) (solo maneja HTTP) y en el [PacienteRepository](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Infrastructure/Repositories/PacienteRepository.cs) (solo maneja datos).
*   **O - Open/Closed (OCP):** El controlador está cerrado a modificación, pero abierto a extensión a través de la inyección de [INotificacionService](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Interfaces/INotificacionService.cs).
*   **L - Liskov Substitution (LSP):** El [EmailNotificacionService](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Infrastructure/Notificaciones/EmailNotificacionService.cs) puede ser reemplazado por un servicio de SMS sin romper el Core.
*   **I - Interface Segregation (ISP):** [IPacienteRepository](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Interfaces/IPacienteRepository.cs) está separada de [INotificacionService](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.Core/Interfaces/INotificacionService.cs).
*   **D - Dependency Inversion (DIP):** El [PacientesController](https://github.com/jolurn/Clinica.API_SOLID_y_Clean_Architecture/blob/master/Clinica.API/Controllers/PacientesController.cs) depende de la interfaz, no de la implementación concreta.

## 🚀 Características Adicionales

*   **Seguridad con DTOs:** Uso de `PacienteCreateDTO` y `PacienteReadDTO` para evitar ataques de *Over-Posting*.
*   **Soft Delete:** Los pacientes nunca se borran físicamente; se desactivan mediante `Activo = false`.
*   **Integridad de Datos:** Configuración de un Índice Único para el DNI mediante **Fluent API**.
*   **Manejo Global de Excepciones:** Middleware configurado para capturar errores del sistema.

## ⚙️ Cómo ejecutar este proyecto localmente

1. Clona este repositorio y abre la solución (`.sln`) en Visual Studio.
2. Configura tu cadena de conexión en `appsettings.json` (proyecto `Clinica.API`).
3. Abre la terminal y aplica las migraciones:
   ```bash
   dotnet ef migrations add Inicial --project Clinica.Infrastructure --startup-project Clinica.API
   dotnet ef database update --project Clinica.Infrastructure --startup-project Clinica.API
