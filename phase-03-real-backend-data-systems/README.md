# Phase 03 - Real Backend Data Systems

This phase focuses on building a real database-driven ASP.NET Core Web API using Entity Framework Core and SQL Server.

The project evolves from basic Web APIs into a production-like backend with database relationships, queries, business rules, reporting, and deployment.

## Project

**Training Center Registration API**

The system manages:

* Students
* Instructors
* Tracks
* Enrollments
* Payments
* Reports

## Phase 03 Tasks

### Task 00 - Workspace & Environment Setup

Prepare the project structure, install EF Core, configure SQL Server, and prepare the development environment.

### Task 01 - EF Core Modeling Drills

Practice Entity Framework Core modeling, relationships, keys, navigation properties, and database configurations.

### Task 02 - Requirements to ERD

Convert business requirements into entities, relationships, and an Entity Relationship Diagram.

### Task 03 - Training Center Database API

Build the main database-driven API with CRUD operations for the training center entities.

### Task 04 - Querying, Filtering & Reporting

Implement filtering, searching, pagination, projections, and report endpoints using EF Core queries.

### Task 05 - Business Rules & Data Integrity

Apply business rules such as enrollment restrictions, capacity validation, duplicate prevention, and payment validation.

### Task 06 - Production Hosting & Remote Database

Deploy the API and connect it to a remote SQL Server database while keeping production secrets secure.

### Task 07 - EF Core API Refactor Pack

Review and refactor intentionally poor EF Core code using better performance, architecture, and data-access practices.

### Task 08 - Interview Demo Pack

Prepare technical explanations, Swagger/Postman evidence, deployment evidence, and demonstrate the completed system.

## Main Project Structure

```text
phase-03-real-backend-data-systems/
│
├── README.md
│
├── task-00-workspace-environment-setup/
├── task-01-ef-core-modeling-drills/
├── task-02-requirements-to-erd/
├── task-03-training-center-database-api/
├── task-04-querying-filtering-reporting/
├── task-05-business-rules-data-integrity/
├── task-06-production-hosting-remote-database/
├── task-07-ef-core-api-refactor-pack/
└── task-08-interview-demo-pack/
```

## Technology Stack

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 8
* SQL Server
* Swagger / OpenAPI
* Postman
* Git & GitHub

## Production Mindset

The phase follows production-oriented backend practices:

* Use Entity Framework Core for database access.
* Use DTOs instead of returning EF entities directly.
* Use migrations for database schema changes.
* Validate business rules.
* Keep connection strings and secrets secure.
* Test API endpoints before submission.
* Deploy the API with a remote database.
* Provide clear evidence for the implemented features.

## Definition of Done

Phase 03 is complete when:

* The API works locally.
* The application is connected to SQL Server.
* Database relationships are correctly modeled.
* EF Core migrations are working.
* CRUD and query endpoints are implemented.
* Business rules are enforced.
* Reports and filtering work correctly.
* The API is deployed online.
* A remote database is configured.
* Swagger is accessible on the deployed API.
* Postman and Swagger evidence are provided.
* The implementation can be explained during review/interview.
