# Task 03 – Secure Platform Upgrade

## Overview
This task upgrades the Phase 03 Training Center API into a secure, role-based platform using ASP.NET Core Web API, JWT authentication, and role-based authorization.

The upgrade protects sensitive endpoints, enforces resource ownership, improves DTO responses, and implements business workflows for Admins, Instructors, and Students.

## Security Improvements
- Removed anonymous access from sensitive endpoints.
- Applied role-based authorization using `[Authorize]` and role restrictions.
- Added ownership validation to prevent access to other users' private data.
- Used the authenticated user's claims to identify the current Student or Instructor.
- Restricted profile updates to permitted fields.
- Protected payment operations and administrative reports.
- Improved API responses using DTOs.
- Updated Swagger and Postman testing evidence.
- Updated the production deployment.

## Admin Workflows

| Endpoint | Description | Access |
|---|---|---|
| `POST /api/tracks` | Create a training track | Admin only |
| `PUT /api/tracks/{id}/assign-instructor` | Assign an instructor to a track | Admin only |
| `GET /api/enrollments` | View and filter enrollments | Admin |
| `PUT /api/enrollments/{id}/status` | Approve or update enrollment status | Admin only |
| `PUT /api/payments/{id}/status` | Update payment status | Admin only |
| `GET /api/reports/revenue-summary` | View revenue summary | Admin only |

### Admin Business Rules
- Only Admin can create and manage training tracks.
- An assigned instructor must exist and be active.
- Enrollment listing supports filtering by status and track.
- Enrollment status transitions must be validated.
- Payment status updates must follow business rules.
- Revenue reports are restricted to Admin.

## Instructor Workflows

| Endpoint | Description | Access |
|---|---|---|
| `GET /api/instructor/my-tracks` | View assigned tracks | Instructor |
| `GET /api/instructor/tracks/{id}/students` | View students in an assigned track | Instructor |
| `POST /api/instructor/tracks/{id}/sessions` | Create a session | Instructor |
| `PUT /api/instructor/sessions/{id}` | Update session information | Instructor |
| `GET /api/instructor/tracks/{id}/progress` | View track progress | Instructor |

### Instructor Security Rules
- Instructors can access only their assigned tracks.
- Track ownership is validated before accessing students, sessions, or progress reports.
- Instructors cannot access another instructor's track resources.
- Instructors cannot access administrative payment revenue reports.

### TrackSession Entity
The `TrackSession` entity supports instructor session management with the following fields:

- `SessionDate`
- `Title`
- `Description`
- `MeetingLink`
- `IsCompleted`
- `CreatedByInstructorId`

## Student Workflows

| Endpoint | Description | Access |
|---|---|---|
| `GET /api/student/me` | View own profile | Student |
| `GET /api/student/my-enrollments` | View own enrollments | Student |
| `GET /api/student/my-payments` | View own payment history | Student |
| `GET /api/tracks/available` | Browse available tracks | Student |
| `POST /api/student/enrollment-requests` | Request enrollment | Student |
| `PUT /api/student/me` | Update permitted profile fields | Student |

### Student Security Rules
- Student identity is obtained from authenticated user claims.
- Students can access only their own profiles, enrollments, and payment history.
- Students cannot retrieve another student's private information.
- Students cannot enroll on behalf of another student.
- Duplicate enrollment in the same active track is prevented.
- Students cannot modify their role, payment status, or administrative fields.
- Profile updates are limited to permitted fields such as name, email, and phone number.

## Authorization and Ownership
The API uses JWT authentication, role-based authorization, and ownership checks to protect resources.

- **401 Unauthorized:** The request has no valid authentication credentials.
- **403 Forbidden:** The authenticated user does not have permission to access the requested resource.
- **404 Not Found:** The requested resource does not exist or cannot be found.

Sensitive endpoints must not allow anonymous access. Authorization is enforced at the API level rather than relying on frontend restrictions.

## Testing and Evidence
Postman is used to verify authentication, authorization, and ownership rules.

Testing evidence should demonstrate:
- Unauthenticated requests are rejected.
- Students cannot access administrative reports.
- Instructors can access their assigned tracks but not other instructors' tracks.
- Students can view their own profiles, enrollments, and payments.
- Students cannot access other students' private data.
- Only Admin can create tracks, assign instructors, and update payment status.
- Enrollment and payment status updates follow the implemented business rules.

Swagger documentation and Postman screenshots are updated to demonstrate the secured API behavior. Screenshots should show the request, response status, and role or collection variable without exposing raw JWT tokens.

## Technologies
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization
- Swagger / OpenAPI
- Postman

## Result
The Training Center API is upgraded into a role-based platform with protected endpoints, ownership validation, instructor-specific workflows, student self-service features, and Admin-controlled business operations.
