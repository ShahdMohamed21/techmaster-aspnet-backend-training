# Phase 03 - Real Backend Data Systems

## Overview

Phase 03 focuses on building a real database-driven backend using ASP.NET Core Web API, EF Core, SQL Server, business rules, querying, reporting, and production deployment.

The phase was completed through Tasks 00–08, covering the full backend workflow from environment setup and database modeling to deployment, refactoring, testing, and interview preparation.

---

## Tech Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* LINQ
* Swagger / OpenAPI
* Postman
* AutoMapper
* SQL Server Remote Database
* ASP.NET Hosting

---

## Phase 03 Tasks

### Task 00 - Workspace & Environment Setup

Completed:

* Phase 03 workspace and task structure.
* EF Core SQL Server packages.
* DbContext configuration.
* Local SQL Server connection.
* Development configuration.
* Initial migrations setup.
* Secret and connection-string safety rules.

---

### Task 01 - EF Core Modeling Drills

Completed 10 EF Core drills covering:

* DbContext and DbSet
* Migrations
* One-to-One relationships
* One-to-Many relationships
* Many-to-Many using a join entity
* Payment relationships
* Seed data
* Soft delete
* Audit fields
* DTO projection
* Pagination

The drills were used to practice EF Core concepts before implementing the main system.

---

### Task 02 - Requirements to ERD

Designed the database based on the Training Center business requirements.

Main entities:

* Student
* Instructor
* TrainingTrack
* Enrollment
* Payment

Main relationships:

```text
Student 1 ──── * Enrollment * ──── 1 TrainingTrack
                                      │
                                      │
                                      1
                                  Instructor

Enrollment 1 ──── * Payment
```

The ERD defines the primary keys, foreign keys, relationships, and business requirements used by the API.

---

### Task 03 - Training Center Database API

Implemented the main Training Center Registration API.

The API manages:

* Students
* Instructors
* Training Tracks
* Enrollments
* Payments
* Reports

Implemented:

* CRUD endpoints
* DTOs
* Service layer
* EF Core queries
* SQL Server persistence
* Swagger documentation
* Postman testing
* Correct HTTP status codes
* Soft delete
* Pagination
* Projection

The API does not expose EF Core entities directly and uses DTOs for request and response models.

---

### Task 04 - Querying, Filtering, Pagination & Reports

Implemented production-style querying patterns including:

* Student search
* Student status filtering
* Pagination
* Track search
* Track level filtering
* Instructor filtering
* Available track capacity
* Enrollment filtering
* Student enrollment history
* Track students
* Unpaid enrollments
* Payment date filtering
* Revenue summary
* Revenue by track
* Top tracks
* Instructor workload
* Students without payments
* Advanced enrollment filtering
* Dashboard summary

Main EF Core concepts used:

* `Where`
* `Contains`
* `Select`
* `CountAsync`
* `Skip`
* `Take`
* `GroupBy`
* `Sum`
* Conditional `IQueryable` composition

---

### Task 05 - Business Rules & Data Integrity

Business rules were implemented inside the service layer to prevent invalid operations.

#### Student Rules

* Unique email.
* Required full name.
* Soft delete.
* Deleted students are excluded from normal queries.
* Inactive/deleted students cannot receive new enrollments.

#### Track Rules

* Required title.
* Unique code.
* Capacity must be greater than zero.
* Valid start and end dates.
* Required instructor.
* Capacity validation.
* Closed tracks cannot accept new enrollments.

#### Enrollment Rules

* Prevent duplicate active enrollments.
* New enrollments start as `Pending`.
* Status transitions are validated.
* Completed enrollments cannot be cancelled directly.
* Cancelled enrollments do not count toward capacity.

#### Payment Rules

* Payment amount must be positive.
* Payment cannot exceed the remaining amount.
* Supported payment statuses are validated.
* Only paid payments contribute to revenue.
* Failed payments do not activate enrollments.

Invalid business operations return `400 Bad Request` with clear error messages.

---

### Task 06 - Production Hosting & Remote Database

Completed the production deployment workflow:

* Local API tested successfully.
* Remote SQL Server database configured.
* Database schema deployed using EF Core migrations.
* API published to ASP.NET hosting.
* Live Swagger configured.
* GET and POST endpoints tested online.
* Remote database connection verified.
* Production credentials kept outside the repository.

Production connection strings are not stored in GitHub.

---

### Task 07 - EF Core/API Refactor Pack

Refactored the provided bad EF Core API code.

The refactor included:

* Async EF Core operations.
* Correct HTTP status codes.
* Request DTOs.
* Response DTOs.
* Service layer.
* Duplicate enrollment validation.
* Track capacity validation.
* Payment validation.
* Soft delete.
* Projection.
* Pagination.
* Improved response/error structure.

The original bad implementation was preserved as required, and the refactored implementation was added separately.

---

### Task 08 - Interview & Demo Pack

Completed the Phase 03 demonstration and interview preparation.

Covered:

* DbContext
* DbSet
* Migrations
* Entities vs DTOs
* Foreign keys
* Entity relationships
* Enrollment as a join entity
* Include
* Projection
* Pagination
* Business rules
* Track capacity
* Payment validation
* Soft delete
* Local vs remote database
* Production configuration
* Connection-string security
* Deployment
* Future improvements

The demo demonstrates the repository structure, ERD, live API, database, endpoints, reports, and business rules.

---

## Architecture

The project follows a controller-service-DTO approach:

```text
Client
  ↓
Controller
  ↓
Service
  ↓
ApplicationDbContext
  ↓
EF Core
  ↓
SQL Server
```

DTOs are used to control the data returned by the API.

---

## Database

The main database contains:

```text
Students
Instructors
TrainingTracks
Enrollments
Payments
__EFMigrationsHistory
```

EF Core migrations are used to manage database schema changes.

---

## API Features

The completed API supports:

* CRUD operations
* Search
* Filtering
* Pagination
* Projection
* Enrollment management
* Payment management
* Business validation
* Soft delete
* Reports
* Dashboard statistics
* Local SQL Server
* Remote SQL Server
* Live Swagger

---

## Testing & Evidence

The phase was tested using:

* Swagger
* Postman
* Local SQL Server
* Remote SQL Server

Evidence includes:

* Swagger screenshots
* Postman requests
* Database screenshots
* Migration evidence
* ERD
* Deployment evidence
* Business-rule validation cases
* Refactoring before/after evidence
* Demo video

---

## Security & Production Practices

* Production secrets are not committed.
* Connection strings are not exposed publicly.
* EF entities are not returned directly.
* Business logic is kept inside services.
* Database changes are managed through migrations.
* Invalid business operations return appropriate errors.

---

## Phase 03 Status

### ✅ Completed

* [x] Task 00 - Workspace & Environment Setup
* [x] Task 01 - EF Core Modeling Drills
* [x] Task 02 - Requirements to ERD
* [x] Task 03 - Training Center Database API
* [x] Task 04 - Querying, Filtering & Reporting
* [x] Task 05 - Business Rules & Data Integrity
* [x] Task 06 - Production Hosting & Remote Database
* [x] Task 07 - EF Core/API Refactor Pack
* [x] Task 08 - Interview & Demo Pack

---

## Final Outcome

Phase 03 transformed the project from a basic Web API into a real database-driven backend system.

The completed system demonstrates:

* Database modeling
* EF Core
* SQL Server
* REST APIs
* DTOs
* Service layer
* LINQ queries
* Pagination
* Reports
* Business rules
* Data integrity
* Soft delete
* Remote database
* Production deployment
* API testing
* Backend interview readiness

**Phase 03 is complete and ready for final review.**
