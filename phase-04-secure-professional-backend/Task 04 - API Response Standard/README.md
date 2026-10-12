# Task 04 – Professional Architecture Refactor

## Overview
This task improves the maintainability and organization of the Training Center API by separating responsibilities into clear layers and introducing consistent API responses and centralized exception handling.

## Project Architecture

The project follows a clean and predictable structure:

```text
TrainingCenter.Api/
├── Controllers/
├── Services/
├── DTOs/
├── Data/
├── Entities/
├── Middleware/
│   └── GlobalExceptionMiddleware.cs
├── Common/
│   └── Responses/
│       └── ApiResponse.cs

```

## Architecture Responsibilities

- **Controllers:** Handle HTTP requests, validate access permissions, and return appropriate HTTP responses.
- **Services:** Contain business logic and application workflows.
- **DTOs:** Control the data received from and returned to API clients.
- **Data:** Contains the database context and data access configuration.
- **Entities:** Represent the application's database models.
- **Middleware:** Handles unexpected exceptions globally.
- **Common/Responses:** Provides a consistent response structure.
- **Helpers:** Contains reusable helper methods.
- **Extensions:** Organizes reusable extension methods and service configuration.
- **Constants:** Stores shared values such as roles and statuses.
- **Validators:** Organizes request validation rules.

## Standard API Response

The `ApiResponse<T>` class provides a consistent structure for successful and failed operations.

### Success Response

```json
{
  "success": true,
  "message": "Track created successfully.",
  "data": {
    "id": 12,
    "title": "ASP.NET Backend Career Training"
  },
  "errors": []
}
```

### Error Response

```json
{
  "success": false,
  "message": "An unexpected error occurred.",
  "data": null,
  "errors": [
    "Please try again later."
  ]
}
```

## Global Exception Handling

The `GlobalExceptionMiddleware` centralizes the handling of unexpected exceptions.

Key responsibilities:
- Catches unhandled exceptions in the request pipeline.
- Logs exception details for debugging and monitoring.
- Returns a consistent JSON error response.
- Uses HTTP `500 Internal Server Error` for unexpected failures.
- Prevents internal exception details and stack traces from being exposed to API clients.

The middleware is registered in `Program.cs` and applies to requests handled later in the pipeline.

## Best Practices

- Keep controllers focused on HTTP handling rather than business-heavy logic.
- Place business rules inside services.
- Use DTOs instead of exposing database entities through sensitive endpoints.
- Return appropriate HTTP status codes.
- Keep API response formats consistent.
- Handle unexpected errors centrally.
- Avoid exposing stack traces or sensitive internal information in production.

## Technologies

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- Middleware
- Dependency Injection
- JSON API Responses

## Result

The Training Center API has a clearer architecture, reusable response handling, and centralized exception management, making the codebase easier to maintain, test, and extend.
