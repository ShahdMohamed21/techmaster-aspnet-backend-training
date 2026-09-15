# Task 03 - Training Center Database API

## Overview

This task implements a database-driven **ASP.NET Core Web API** for TechMaster Academy. The system manages students, instructors, training tracks, enrollments, and payments using **SQL Server** and **Entity Framework Core**.

The API uses **DTOs, Services, Controllers, Swagger, and Postman** to provide a clean and maintainable backend architecture.

## Main Entities

- **Student:** Stores student profile information and enrollment data.
- **Instructor:** Stores instructor information and assigned training tracks.
- **TrainingTrack:** Represents available training tracks, capacity, dates, status, and instructor assignment.
- **Enrollment:** Connects students with training tracks and stores enrollment status and progress.
- **Payment:** Stores payment transactions associated with enrollments.

## API Areas

- **Students:** Create, update, retrieve, search, filter, and soft-delete students.
- **Instructors:** Manage instructors and retrieve their assigned tracks.
- **Training Tracks:** Manage tracks, instructors, capacity, and track status.
- **Enrollments:** Manage student registrations, status changes, and enrollment history.
- **Payments:** Create and manage payments and payment history.
- **Reports:** Provide dashboard statistics, unpaid enrollments, track capacity, and revenue summaries.

## Architecture

The API follows a layered structure:

- **Controllers:** Handle HTTP requests and responses.
- **Services:** Contain business logic and application rules.
- **DTOs:** Control the data exposed through the API.
- **Entities:** Represent database tables.
- **Data:** Contains `DbContext` and database configuration.
- **Migrations:** Manage database schema changes.

## Database

- **SQL Server** is used as the database.
- **Entity Framework Core** is used for ORM and database operations.
- EF Core migrations are used to create and update the database schema.

## Business Rules

- Student emails must be unique.
- Track codes must be unique.
- A student cannot enroll in the same track more than once.
- A student cannot enroll when the track reaches its capacity.
- Enrollment status changes must follow valid transitions.
- Students and tracks use soft delete where applicable.
- Payments are linked to enrollments.
- Financial amounts use decimal values.

## API Documentation & Testing

- **Swagger/OpenAPI** is used to document and explore the API.
- **Postman** is used to test API endpoints.
- Success and failure test cases are documented as evidence.

## Response Format

API responses follow a consistent structure:

```json
{
  "success": true,
  "message": "Student created successfully.",
  "data": {}
}
