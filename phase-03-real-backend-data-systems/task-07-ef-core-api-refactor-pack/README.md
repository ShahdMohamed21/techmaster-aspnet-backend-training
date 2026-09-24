# Task 07 - EF Core / API Refactor Pack

## Overview

This task focuses on refactoring a poorly designed Enrollment and Payment API.

The original implementation had several issues related to database access, validation, API design, error handling, and separation of concerns.

The goal of this refactor was to preserve the original bad implementation for comparison and implement a cleaner, more maintainable, and more efficient version.

---

# 1. Problems in the Original Implementation

The original implementation had the following problems:

### 1. Synchronous Database Operations

The original code used synchronous EF Core operations such as:

```csharp
ToList()
FirstOrDefault()
SaveChanges()
```

This can block request threads and reduce scalability.

### 2. Business Logic Inside the Controller

The controller directly handled database operations and business rules.

This made the controller difficult to maintain and test.

### 3. Entity Models Used Directly as Request Models

The original `Create` endpoint accepted:

```csharp
Enrollment enrollment
```

This exposed the database entity directly to the API.

### 4. Entities Returned Directly

The API returned EF Core entities instead of dedicated response DTOs.

This can expose internal database structure and unnecessary fields.

### 5. No Duplicate Enrollment Validation

The original implementation allowed the same student to be enrolled in the same track multiple times.

### 6. No Track Capacity Validation

There was no validation to prevent enrollment after the training track reached its capacity.

### 7. Incorrect HTTP Status Codes

The original implementation returned `200 OK` even when resources were not found.

For example:

```csharp
return Ok("not found");
```

A missing resource should return `404 Not Found`.

### 8. Hard Delete

The original delete endpoint permanently removed the enrollment:

```csharp
_db.Enrollments.Remove(item);
```

This causes the data to be physically deleted from the database.

### 9. No Payment Amount Validation

The original payment endpoint did not validate whether the payment amount was valid.

It could accept zero or negative amounts.

### 10. No Pagination

The original `GetAll` endpoint loaded all enrollments from the database.

This can become inefficient when the number of records grows.

### 11. Loading Full Entities

The original implementation used `Include()` and loaded complete entities when only a subset of fields was required.

### 12. No Consistent Error Response

Different endpoints returned different error formats.

The refactored implementation uses a consistent structure:

```json
{
  "success": false,
  "message": "..."
}
```

---

# 2. Improvements Implemented

## 1. Async EF Core

Database operations were changed to asynchronous operations:

```csharp
ToListAsync()
FirstOrDefaultAsync()
AnyAsync()
CountAsync()
SaveChangesAsync()
```

This improves scalability and avoids blocking request threads.

---

## 2. Service Layer

Business logic was moved from controllers into services.

The architecture is now:

```text
Controller
    ↓
Service
    ↓
ApplicationDbContext
    ↓
Database
```

This improves separation of concerns and makes the code easier to test and maintain.

---

## 3. Request DTOs

Dedicated request DTOs were introduced.

Example:

```csharp
CreateEnrollmentRequest
```

Instead of accepting the database entity directly.

---

## 4. Response DTOs

Dedicated response DTOs were introduced, including:

```csharp
EnrollmentDetailsResponse
EnrollmentListItemResponse
PaymentResponse
```

This prevents exposing EF Core entities directly through the API.

---

## 5. Duplicate Enrollment Validation

Before creating an enrollment, the service checks whether the student is already enrolled in the selected track.

```csharp
var alreadyEnrolled = await _context.Enrollments
    .AnyAsync(e =>
        e.StudentId == request.StudentId &&
        e.TrainingTrackId == request.TrainingTrackId &&
        !e.IsDeleted &&
        e.Status != "Cancelled");
```

---

## 6. Track Capacity Validation

The service checks the number of active enrollments before creating a new enrollment.

```csharp
var activeEnrollments = await _context.Enrollments
    .CountAsync(e =>
        e.TrainingTrackId == request.TrainingTrackId &&
        !e.IsDeleted &&
        e.Status == "Active");
```

The enrollment is rejected when the track reaches its capacity.

---

## 7. Payment Validation

Payment creation now validates:

* Payment amount
* Payment method
* Reference number
* Enrollment existence
* Duplicate reference numbers

For example:

```csharp
if (request.Amount <= 0)
{
    throw new ArgumentException(
        "Payment amount must be greater than zero");
}
```

---

## 8. Correct HTTP Status Codes

The refactored API uses appropriate HTTP status codes.

| Situation           |       Status Code |
| ------------------- | ----------------: |
| Successful GET      |          `200 OK` |
| Successful creation |     `201 Created` |
| Invalid request     | `400 Bad Request` |
| Resource not found  |   `404 Not Found` |

---

## 9. Soft Delete

Enrollment deletion was changed from hard delete to soft delete.

Instead of removing the record:

```csharp
_context.Enrollments.Remove(enrollment);
```

the service now updates:

```csharp
enrollment.IsDeleted = true;
enrollment.DeletedAt = DateTime.UtcNow;
```

Queries exclude deleted records:

```csharp
.Where(e => !e.IsDeleted)
```

This preserves historical data.

---

## 10. Projection

The refactored queries use `Select()` to retrieve only the required fields.

Example:

```csharp
.Select(e => new EnrollmentListItemResponse
{
    EnrollmentId = e.EnrollmentId,
    StudentId = e.StudentId,
    StudentName = e.Student.FullName,
    TrainingTrackId = e.TrainingTrackId,
    TrainingTrackTitle = e.TrainingTrack.Title,
    Status = e.Status,
    ProgressPercentage = e.ProgressPercentage
})
```

This avoids loading unnecessary entity data.

---

## 11. Pagination

The enrollment list endpoint now supports:

```text
pageNumber
pageSize
```

Example:

```http
GET /api/enrollments?pageNumber=1&pageSize=10
```

The query uses:

```csharp
Skip((pageNumber - 1) * pageSize)
.Take(pageSize)
```

The response also includes:

```text
TotalCount
TotalPages
PageNumber
PageSize
```

---

## 12. Improved Error Response

API errors now follow a consistent structure:

```json
{
  "success": false,
  "message": "Enrollment was not found"
}
```

Successful responses follow:

```json
{
  "success": true,
  "data": {}
}
```

---

# 3. Before vs After

## Before

```text
Controller
    ↓
ApplicationDbContext
    ↓
Database
```

The controller contained:

* Database queries
* Business logic
* Validation
* Entity creation
* Entity deletion

---

## After

```text
Controller
    ↓
Service
    ↓
ApplicationDbContext
    ↓
Database
```

The controller is now responsible mainly for:

* Receiving HTTP requests
* Calling the service
* Returning HTTP responses

The service handles:

* Business rules
* Validation
* Database operations
* DTO projection

---

# 4. API Endpoints

## Enrollment

### Get All Enrollments

```http
GET /api/enrollments
```

Supports:

```text
status
trackId
studentId
paymentStatus
pageNumber
pageSize
```

Example:

```http
GET /api/enrollments?status=Active&trackId=1&pageNumber=1&pageSize=10
```

### Get Enrollment

```http
GET /api/enrollments/{id}
```

### Create Enrollment

```http
POST /api/enrollments
```

Example:

```json
{
  "studentId": 1,
  "trainingTrackId": 1
}
```

### Update Enrollment Status

```http
PUT /api/enrollments/{id}/status
```

### Delete Enrollment

```http
DELETE /api/enrollments/{id}
```

The delete operation uses soft delete.

---

# 5. Payment Endpoints

### Get Payments

```http
GET /api/payments
```

Supports:

```text
fromDate
toDate
status
```

### Get Payment

```http
GET /api/payments/{id}
```

### Create Payment

```http
POST /api/payments
```

Example:

```json
{
  "enrollmentId": 1,
  "amount": 1500,
  "paymentMethod": "Cash",
  "referenceNumber": "PAY-001",
  "notes": "First payment"
}
```

### Get Enrollment Payments

```http
GET /api/enrollments/{id}/payments
```

### Update Payment Status

```http
PUT /api/payments/{id}/status
```

---

# 6. EF Core Concepts Used

The refactor applies several EF Core best practices:

* `AsNoTracking()`
* `Where()`
* `AnyAsync()`
* `CountAsync()`
* `FirstOrDefaultAsync()`
* `ToListAsync()`
* `Skip()`
* `Take()`
* Projection with `Select()`
* Async `SaveChangesAsync()`
* Soft delete filtering

---

# 7. Validation Scenarios

The API validates several invalid scenarios.

### Invalid Enrollment

```text
Student does not exist
Student is inactive
Student is deleted
Training track does not exist
Training track is deleted
Training track is not open
Student is already enrolled
Track capacity is reached
```

### Invalid Payment

```text
Amount <= 0
Missing payment method
Missing reference number
Enrollment does not exist
Duplicate reference number
Invalid payment status
```

---

# 8. Evidence

The following screenshots should be included as task evidence:

### Before Refactoring

* Original bad Enrollment controller
* Direct database access
* Hard delete
* Synchronous EF Core operations

### After Refactoring

* Refactored Enrollment controller
* Enrollment service
* Payment service
* Pagination response
* Soft delete implementation
* Swagger successful requests
* Swagger validation/error responses

Screenshots can be placed in:

```text
/docs/screenshots/
```

Suggested files:

```text
before-enrollment-controller.png
after-enrollment-controller.png
enrollment-pagination.png
duplicate-enrollment-error.png
capacity-error.png
payment-validation.png
soft-delete.png
```

---

# 9. Conclusion

The refactored implementation improves the original API by introducing:

* Separation of concerns
* Async EF Core operations
* DTO-based API contracts
* Business validation
* Proper HTTP status codes
* Pagination
* Projection
* Soft delete
* Consistent API responses
* Service-based architecture

The original implementation is preserved for comparison and evidence, while the refactored implementation provides the improved API behavior.
