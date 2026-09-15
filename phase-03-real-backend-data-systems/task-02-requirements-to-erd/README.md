# Task 02 - Requirements to ERD

## Overview

This task translates the business requirements of **TechMaster Academy** into a relational database design. The ERD defines the main entities, fields, primary keys, foreign keys, and relationships required to manage students, instructors, training tracks, enrollments, and payments.

## Main Entities & Primary/Foreign Keys

- **Student:** Stores student profile information and account status.
  - **PK:** `StudentId`

- **Instructor:** Stores instructor information and specialization details.
  - **PK:** `InstructorId`

- **TrainingTrack:** Represents training tracks, their capacity, dates, status, and assigned instructor.
  - **PK:** `TrainingTrackId`
  - **FK:** `InstructorId` → `Instructor.InstructorId`

- **Enrollment:** Connects students with training tracks and stores enrollment status and progress.
  - **PK:** `EnrollmentId`
  - **FK:** `StudentId` → `Student.StudentId`
  - **FK:** `TrainingTrackId` → `TrainingTrack.TrainingTrackId`

- **Payment:** Stores payment transactions related to enrollments.
  - **PK:** `PaymentId`
  - **FK:** `EnrollmentId` → `Enrollment.EnrollmentId`

## Relationships

- `Student` **1 → Many** `Enrollment`
- `Instructor` **1 → Many** `TrainingTrack`
- `TrainingTrack` **1 → Many** `Enrollment`
- `Enrollment` **1 → Many** `Payment`

## Key Business Rules

- A student can enroll in multiple training tracks.
- A training track can have multiple students.
- Each training track has one main instructor.
- An instructor can teach multiple training tracks.
- An enrollment can have multiple payments.
- Student and track records support soft deletion where required.
- Student emails, instructor emails, and track codes must be unique.

## Deliverables

- ERD Diagram
- Tables and fields
- Primary and foreign keys
- Relationship definitions
- Business rules
- Business questions supported by the database design
