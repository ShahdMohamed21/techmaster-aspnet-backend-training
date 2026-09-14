# Task 00 - Workspace & Environment Setup

## Objective

Prepare the Phase 03 workspace and development environment for building the Training Center database-driven API.

## Completed Setup

* Created the Phase 03 folder structure.
* Created the required task folders.
* Created the Training Center ASP.NET Core Web API project.
* Installed Entity Framework Core packages.
* Configured SQL Server support.
* Created and registered `AppDbContext`.
* Prepared the local SQL Server connection string.
* Configured Swagger and API startup.
* Added the required project structure.
* Prepared the project for EF Core migrations.
* Kept production credentials out of the repository.

## Technology

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* SQL Server
* Swagger / OpenAPI

## Required EF Core Packages

```text
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
```

EF Core CLI tool:

```bash
dotnet-ef
```

## Project Structure

```text
TrainingCenter.Api/
│
├── Controllers/
├── Data/
│   └── AppDbContext.cs
├── Entities/
├── DTOs/
├── Students/
├── Tracks/
├── Enrollments/
├── Payments/
├── Reports/
├── Services/
├── Common/
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

## Local Setup

1. Install SQL Server.
2. Configure the local `DefaultConnection`.
3. Restore NuGet packages.
4. Run the application.
5. Open Swagger.

Example local connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TechMasterTrainingCenterDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## EF Core Commands

After the database models are ready:

```bash
dotnet ef migrations add InitialTrainingCenterSchema
```

Then:

```bash
dotnet ef database update
```

## Security

Production connection strings and passwords must not be committed to GitHub.

Production configuration will be provided through the hosting provider's environment/configuration settings.

Passwords and sensitive connection information should never be included in screenshots or README files.

## Acceptance Criteria

* [x] Project runs locally.
* [x] EF Core packages are installed.
* [x] SQL Server provider is configured.
* [x] `AppDbContext` is registered.
* [x] Local database connection is prepared.
* [x] No production secrets are committed.
* [x] README contains local setup instructions.
* [x] Project is ready for EF Core modeling and database implementation.

## Status

**Completed - Ready for Task 01**
