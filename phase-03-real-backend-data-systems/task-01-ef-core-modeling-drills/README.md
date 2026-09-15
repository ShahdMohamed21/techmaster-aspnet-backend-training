# Task 01 - EF Core Modeling Drills

This task contains 10 EF Core modeling drills covering the main database concepts required for the Training Center API.

> **Note:** This task contains the screenshots and evidence only.
> The actual implementation and code for all drills are available in **Task 00 - Workspace Environment Setup**.

## Completed Drills

### Drill 01 - DbContext & First Migration

* Created `Student` entity.
* Configured `AppDbContext` and `DbSet<Student>`.
* Created and applied `InitialStudentSchema` migration.
* Verified the `Students` table in SQL Server.

### Drill 02 - One-to-One Student Profile

* Created `Student` and `StudentProfile`.
* Configured a one-to-one relationship.
* Added `StudentId` as the foreign key.
* Created `AddStudentProfile` migration.

### Drill 03 - One-to-Many Instructor & Tracks

* Created `Instructor` and `TrainingTrack`.
* Configured one instructor to have many tracks.
* Added `InstructorId` foreign key.
* Created `AddInstructorsAndTracks` migration.
* Added an endpoint to retrieve an instructor's tracks.

### Drill 04 - Many-to-Many Enrollment

* Created `Enrollment` as a join entity between Students and Tracks.
* Added `Status`, `EnrollmentDate`, and `FinalGrade`.
* Configured foreign keys and navigation properties.
* Created `AddEnrollments` migration.
* Added endpoints for student tracks and track students.

### Drill 05 - Payment Summary

* Created `PaymentSummary`.
* Configured a one-to-one relationship with `Enrollment`.
* Added decimal payment fields and `PaymentStatus`.
* Configured `EnrollmentId` as a unique foreign key.
* Created `AddPaymentSummary` migration.

### Drill 06 - Seed Data

* Added seed data using EF Core `HasData`.
* Seeded students, instructors, tracks, and enrollments.
* Used fixed IDs and deterministic data.
* Verified seed data through API/database.

### Drill 07 - Soft Delete

* Added `IsDeleted` and `DeletedAt` to `Student`.
* DELETE marks the student as deleted instead of removing the row.
* GET excludes deleted students by default.
* Added an option to include deleted records.
* Created `AddSoftDeleteFields` migration.

### Drill 08 - Audit Fields

* Added `CreatedAt` and `UpdatedAt`.
* `CreatedAt` is set during creation.
* `UpdatedAt` is set during updates.
* UTC time is used.
* Audit fields are handled by `StudentService`.

### Drill 09 - Projection DTO

* Created `StudentListItemDto`.
* Created `TrackDetailsDto`.
* Used LINQ `Select` projection.
* Avoided returning EF entities directly.
* Returned only the required fields.

### Drill 10 - Pagination

* Added `pageNumber` and `pageSize` query parameters.
* Validated pagination inputs.
* Used `CountAsync`, `Skip`, and `Take`.
* Returned pagination metadata using `PaginationResult<T>`.
* Implemented server-side pagination.

## Evidence

Screenshots for the completed drills are included in this task, covering:

* Database tables and relationships
* Migrations
* Seed data
* Soft delete behavior
* Audit fields
* DTO projection responses
* Pagination responses and validation

**Implementation:** Task 00 - Workspace Environment Setup
**Evidence:** Task 01 - EF Core Modeling Drills
