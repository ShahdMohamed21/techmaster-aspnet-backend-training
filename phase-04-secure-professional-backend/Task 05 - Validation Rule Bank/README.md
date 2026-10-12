# Task 05 – Validation, Error Handling & Logging

## Overview
This task focuses on improving API reliability and security by implementing request validation, consistent error handling, and structured logging across the Training Center API.

## 1. Validation
Implemented validation to ensure incoming requests and business operations follow the required rules.

- Required fields and valid email format.
- Password strength and password confirmation.
- Positive capacity, IDs, and payment amounts.
- Valid date ranges.
- Duplicate enrollment prevention.
- Track capacity validation.
- Track and instructor existence checks.
- Enrollment status validation.
- Role-based access control.
- Ownership validation to prevent unauthorized access to other users' resources.
- Profile update restrictions to prevent unauthorized role changes.

## 2. Global Error Handling
Implemented `GlobalExceptionMiddleware` to handle unexpected exceptions centrally.

- Logs unhandled exceptions internally.
- Returns a consistent JSON error response.
- Returns HTTP `500 Internal Server Error` for unexpected errors.
- Prevents internal exception details and stack traces from being exposed to clients.
- Includes a Trace ID to help identify errors during debugging.
- Preserves appropriate HTTP status codes for validation and authorization errors.

### HTTP Status Codes

| Status Code | Description |
|---|---|
| `400 Bad Request` | Invalid request data |
| `401 Unauthorized` | Missing or invalid authentication |
| `403 Forbidden` | Insufficient permissions |
| `404 Not Found` | Resource does not exist |
| `409 Conflict` | Duplicate or conflicting resource |
| `500 Internal Server Error` | Unexpected server error |

## 3. Logging
Added logging to monitor important operations and diagnose problems.

- Successful and failed login attempts.
- Registration events.
- Password change operations.
- Enrollment creation, status updates, and deletion.
- Track creation, updates, and deletion.
- Unexpected exceptions.

**Security:** Passwords, access tokens, refresh tokens, and other sensitive credentials are not written to logs.

## 4. Testing & Evidence
The following scenarios were tested using Swagger:

| Test Case | Expected Result |
|---|---|
| Invalid email during registration | `400 Bad Request` |
| Invalid login credentials | `401 Unauthorized` |
| Request without an authentication token | `401 Unauthorized` |
| Student accessing an Admin-only endpoint | `403 Forbidden` |
| Request for a non-existing enrollment | `404 Not Found` |

Screenshots are included as evidence for validation and error-handling behavior.

## 5. Technologies Used
- ASP.NET Core Web API
- C#
- Data Annotations
- Middleware
- `ILogger<T>`
- JWT Authentication and Role-Based Authorization
- Swagger / OpenAPI

## Conclusion
Task 05 improves API security, consistency, and maintainability through request and business-rule validation, centralized exception handling, structured logging, and HTTP status code management.
