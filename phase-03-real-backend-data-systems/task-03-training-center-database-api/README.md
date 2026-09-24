# Task 03 - Training Center Database API 🎓

A database-driven Training Center REST API built with ASP.NET Core, Entity Framework Core, and SQL Server.

The system was developed for TechMaster Academy to manage students, instructors, training tracks, enrollments, payments, and business reports through a structured and validated RESTful API.

---

## 📋 Table of Contents

* [Project Overview](#-project-overview)
* [Technical Stack](#-technical-stack)
* [Project Structure](#-project-structure)
* [Database Design](#-database-design)
* [API Modules](#-api-modules)
* [DTOs and Services](#-dtos-and-services)
* [Business Rules](#-business-rules)
* [Reports](#-reports)
* [API Response](#-api-response)
* [Database and Migrations](#-database-and-migrations)
* [Testing and Documentation](#-testing-and-documentation)
* [Task Status](#-task-status)

---

## 🎯 Project Overview

The Training Center API provides the backend system required to manage the main operations of a training academy.

The system manages:

* Students
* Instructors
* Training Tracks
* Enrollments
* Payments
* Business Reports

All application data is stored in SQL Server and accessed through Entity Framework Core.

The API follows a layered approach using Controllers, Services, DTOs, and EF Core to keep responsibilities separated and make the system easier to maintain.

---

## 🛠 Technical Stack

| Technology               | Usage                      |
| ------------------------ | -------------------------- |
| ASP.NET Core Web API     | Building REST APIs         |
| C#                       | Application development    |
| Entity Framework Core    | Database access            |
| SQL Server               | Data storage               |
| LINQ                     | Queries and reporting      |
| Swagger / OpenAPI        | API documentation          |
| Postman                  | API testing                |
| EF Core Migrations       | Database schema management |
| MonsterASP.NET / Hosting | API deployment             |

---

## 📁 Project Structure

```text
task-03-training-center-database-api/
│
├── README.md
├── TrainingCenter.Api/
│   │
│   ├── Controllers/
│   ├── Data/
│   ├── Entities/
│   ├── DTOs/
│   ├── Services/
│   ├── Common/
│   ├── Migrations/
│   └── Program.cs
│
├── postman/
└── evidence/
```

### Main Application Layers

```text
Client
   │
   ▼
Controllers
   │
   ▼
DTOs
   │
   ▼
Services
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

Controllers handle HTTP requests and responses, while Services contain the business logic and communicate with the database through EF Core.

---

## 🗄 Database Design

The main entities are:

* `Student`
* `Instructor`
* `TrainingTrack`
* `Enrollment`
* `Payment`

### Relationships

```text
Instructor
    │
    │ 1 : Many
    ▼
TrainingTrack
    │
    │ 1 : Many
    ▼
Enrollment
    │
    │ 1 : Many
    ▼
Payment

Student
    │
    │ 1 : Many
    ▼
Enrollment
```

### Relationship Explanation

A student can enroll in multiple training tracks through the `Enrollment` entity.

A training track can contain multiple enrollments.

The `Enrollment` entity connects `Student` and `TrainingTrack` and stores additional information such as:

* EnrollmentDate
* Status
* ProgressPercentage
* FinalResult

Payments are connected to enrollments, allowing each enrollment to have multiple payment records.

---

## 👨‍🎓 Students API

The Students module supports the student lifecycle.

### Main Operations

```text
GET     /api/students
GET     /api/students/{id}
POST    /api/students
PUT     /api/students/{id}
DELETE  /api/students/{id}
```

### Features

* Paginated student listing.
* Search and active-status filtering.
* Student details.
* Unique email validation.
* Student updates.
* Soft deletion.
* Enrollment summary.

---

## 👨‍🏫 Instructors API

The Instructors module manages instructors and their assigned tracks.

```text
GET     /api/instructors
GET     /api/instructors/{id}
POST    /api/instructors
PUT     /api/instructors/{id}
GET     /api/instructors/{id}/tracks
```

### Features

* Instructor creation and update.
* Instructor details.
* Email uniqueness validation.
* Retrieve tracks assigned to an instructor.

---

## 📚 Training Tracks API

Training tracks represent the courses offered by the training center.

```text
GET     /api/tracks
GET     /api/tracks/{id}
POST    /api/tracks
PUT     /api/tracks/{id}
DELETE  /api/tracks/{id}
```

### Supported Filters

* Keyword
* Level
* Status
* InstructorId

### Features

* Track creation and update.
* Instructor assignment.
* Capacity validation.
* Start and end date validation.
* Capacity summary.
* Soft deletion according to enrollment rules.

---

## 📝 Enrollment API

Enrollments connect students with training tracks.

```text
GET     /api/enrollments
GET     /api/enrollments/{id}
POST    /api/enrollments
PUT     /api/enrollments/{id}/status
GET     /api/students/{id}/enrollments
GET     /api/tracks/{id}/students
```

### Enrollment Filters

The API supports filtering by:

* Status
* Track ID
* Student ID
* Payment Status

### Enrollment Rules

Before creating an enrollment, the system checks:

1. The student exists and is active.
2. The training track exists.
3. The track is open.
4. The student is not already enrolled.
5. The track still has available capacity.

---

## 💳 Payments API

Payments are associated with student enrollments.

```text
GET     /api/payments
POST    /api/payments
GET     /api/enrollments/{id}/payments
PUT     /api/payments/{id}/status
```

### Features

* Payment creation.
* Payment history.
* Date range filtering.
* Payment status filtering.
* Payment status updates.
* Reference number validation.
* Amount validation.

Supported payment statuses include:

```text
Pending
Paid
Failed
Refunded
```

---

## 📊 Reports API

The Reports module provides aggregated business information.

```text
GET /api/reports/dashboard-summary
GET /api/reports/unpaid-enrollments
GET /api/reports/track-capacity
GET /api/reports/revenue-summary
GET /api/reports/revenue-by-track
```

### Report Examples

#### Dashboard Summary

Provides high-level numbers such as:

* Total students.
* Active students.
* Total tracks.
* Active tracks.
* Total enrollments.
* Active enrollments.
* Paid revenue.

#### Track Capacity

Shows:

* Track capacity.
* Current enrollment count.
* Available seats.
* Full-track status.

#### Revenue Reports

Provides:

* Total revenue.
* Number of paid transactions.
* Revenue by track.
* Revenue based on payment data.

---

## 🧩 DTOs and Services

The API does not rely on returning EF Core entities directly.

### Request DTOs

Examples:

```text
CreateStudentRequest
UpdateStudentRequest
CreateTrackRequest
CreateEnrollmentRequest
CreatePaymentRequest
```

### Response DTOs

Examples:

```text
StudentListItemResponse
StudentDetailsResponse
TrackDetailsResponse
EnrollmentDetailsResponse
PaymentResponse
Report DTOs
```

DTOs control which data is received from and returned to the client.

---

## 🛡 Business Rules

The system contains validation rules to protect data consistency.

### Student

* Email must be unique.
* Deleted students are excluded from normal operations.

### Instructor

* Email must be unique.
* Only valid instructors can be assigned where required.

### Training Track

* Capacity must be greater than zero.
* Start date must be before end date.
* Capacity cannot conflict with existing active enrollments.

### Enrollment

* Duplicate active enrollment is not allowed.
* Enrollment cannot exceed track capacity.
* Only valid/open tracks can accept new enrollments.

### Payment

* Amount must be greater than zero.
* Enrollment must exist.
* Reference number must be unique.
* Payment status must be valid.

---

## 🗑 Soft Delete

The system uses soft deletion for entities that should not be permanently removed.

Instead of deleting the database row, the application updates:

```text
IsDeleted = true
DeletedAt = current date/time
```

Normal queries then filter out deleted records.

This keeps historical information available in the database.

---

## 🔄 EF Core and Database

Entity Framework Core is responsible for communicating with SQL Server.

The project uses:

* `DbContext`
* `DbSet`
* LINQ
* Async queries
* Migrations
* Projections
* Foreign keys
* Entity relationships

Database schema changes are managed through EF Core migrations.

Example:

```powershell
Add-Migration MigrationName
Update-Database
```

---

## 📄 API Response Structure

The API returns structured responses containing success information and the requested data.

Example:

```json
{
  "success": true,
  "data": {
    "enrollmentId": 1,
    "studentId": 2,
    "trainingTrackId": 3,
    "status": "Pending"
  }
}
```

For invalid requests or missing resources, the API returns appropriate HTTP status codes with clear error messages.

---

## 🔎 Pagination and Projection

Large collections are handled using pagination.

For example:

```text
pageNumber = 1
pageSize = 10
```

EF Core uses:

```text
Skip()
Take()
```

The API also uses `Select` projection to return only the required fields instead of loading complete entities.

This reduces unnecessary data retrieval.

---

## 🧪 Testing and Verification

The API can be tested through:

### Swagger

Swagger provides interactive API documentation and allows endpoints to be executed directly from the browser.

### Postman

Postman is used to verify API behavior, including:

* Successful requests.
* Validation failures.
* Not-found scenarios.
* Business rule violations.
* CRUD operations.

Evidence for API testing is stored in the `postman/` and `evidence/` folders.

---

## 🚀 Running the Project

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022 or VS Code

### Restore Packages

```bash
dotnet restore
```

### Apply Database Migrations

```bash
dotnet ef database update
```

### Run the API

```bash
dotnet run
```

Then open Swagger:

```text
https://localhost:<port>/swagger
```

---

## 🌐 Database Environments

The project supports working with:

* Local SQL Server during development.
* Remote SQL Server for deployment.

The connection string is configured through application configuration and should not contain publicly exposed credentials.

---

## 📸 Evidence

The project evidence can include:

* Swagger screenshots.
* Postman requests and responses.
* Database tables.
* Migration results.
* Remote database evidence.
* API deployment evidence.

---

## 🏁 Task Status

### Completed ✅

The Training Center API implements the required Phase 03 functionality, including:

* Student management.
* Instructor management.
* Training track management.
* Enrollment management.
* Payment management.
* Business validation.
* DTO-based API responses.
* Service-layer architecture.
* EF Core database access.
* SQL Server integration.
* Soft deletion.
* Pagination and projection.
* Reporting endpoints.
* Swagger documentation.
* Postman testing.
