# Task 04 - Querying, Filtering, Pagination & Reports 🔎📊

A production-style querying and reporting implementation for the Training Center API.

This task focuses on building practical API queries using Entity Framework Core, including filtering, searching, sorting, pagination, projections, conditional query composition, and business reports.

The queries are based on real Training Center business requirements and return DTO-based responses instead of exposing database entities directly.

---

## 📋 Table of Contents

* [Task Overview](#-task-overview)
* [Objectives](#-objectives)
* [Technical Concepts](#-technical-concepts)
* [Student Queries](#-student-queries)
* [Track Queries](#-track-queries)
* [Enrollment Queries](#-enrollment-queries)
* [Payment Queries](#-payment-queries)
* [Reporting Queries](#-reporting-queries)
* [Advanced Querying](#-advanced-querying)
* [Pagination](#-pagination)
* [Projection](#-projection)
* [Validation](#-validation)
* [Testing](#-testing)
* [Selected Query Explanations](#-selected-query-explanations)
* [Task Status](#-task-status)

---

## 🎯 Task Overview

The purpose of Task 04 is to extend the Training Center API with production-style querying capabilities.

The implementation covers:

* Searching.
* Filtering.
* Pagination.
* Sorting.
* Conditional filters.
* Related-data queries.
* Aggregations.
* Revenue calculations.
* Business reports.
* DTO projections.

The target is to implement at least 15 query/report specifications, with all 20 specifications implemented for the complete task.

---

## 🎯 Objectives

The main objectives of this task are:

* Build reusable and safe query patterns.
* Use EF Core LINQ effectively.
* Avoid returning complete entity graphs.
* Return DTO-shaped responses.
* Validate query parameters.
* Support multiple filters in the same endpoint.
* Build reports from business requirements.
* Use asynchronous database operations.
* Document and test query endpoints.

---

## 🛠 Technical Concepts

The implementation uses several EF Core querying techniques:

```text
Where()
Contains()
Any()
All()
Select()
CountAsync()
SumAsync()
GroupBy()
OrderBy()
OrderByDescending()
Skip()
Take()
IQueryable
```

These techniques are combined to create efficient API queries.

---

# 👨‍🎓 Student Queries

## Query 01 - Search Students

```http
GET /api/students?search=mohamed
```

Searches students by:

* Full Name
* Email
* Phone

The query uses case-insensitive matching and returns a student DTO containing only the required fields.

### EF Core Concepts

```text
Where
Contains
Select
```

---

## Query 02 - Filter Students By Status

```http
GET /api/students?isActive=true
```

Returns students based on their active status.

Deleted students are excluded from normal results.

### EF Core Concepts

```text
Where
IsActive
IsDeleted
```

---

## Query 03 - Paged Students List

```http
GET /api/students?pageNumber=1&pageSize=10
```

Returns students in pages instead of loading the complete dataset.

The response contains:

* Items
* TotalCount
* TotalPages
* PageNumber
* PageSize

### EF Core Concepts

```text
CountAsync
Skip
Take
```

---

# 📚 Training Track Queries

## Query 04 - Search Tracks

```http
GET /api/tracks?keyword=backend
```

Searches tracks using:

* Title
* Code
* Description

Only valid active/open tracks are returned by default.

---

## Query 05 - Filter Tracks By Level

```http
GET /api/tracks?level=Beginner
```

Filters tracks based on their level.

Invalid level values are rejected when validation is enabled.

---

## Query 06 - Filter Tracks By Instructor

```http
GET /api/tracks?instructorId=2
```

Returns tracks assigned to a specific instructor.

The API uses a documented policy for cases where the instructor does not exist or has no assigned tracks.

---

## Query 07 - Tracks With Available Seats

```http
GET /api/reports/tracks-with-available-seats
```

Returns tracks where the capacity is greater than the number of active enrollments.

The response includes:

* Track
* Capacity
* Active Enrollments
* Remaining Seats

### EF Core Concepts

```text
Count
Group
Projection
```

---

# 📝 Enrollment Queries

## Query 08 - Enrollment List With Details

```http
GET /api/enrollments
```

Returns enrollment information together with:

* Student Name
* Track Title
* Status

Instead of returning the complete entity graph, the endpoint returns only the required DTO fields.

---

## Query 09 - Filter Enrollments By Status

```http
GET /api/enrollments?status=Pending
```

Supported statuses include:

```text
Pending
Active
Completed
Cancelled
```

The query filters enrollments based on their current status.

---

## Query 10 - Student Enrollment History

```http
GET /api/students/{id}/enrollments
```

Returns all enrollments belonging to a specific student together with track information.

If the student does not exist, the API returns an appropriate not-found response.

---

## Query 11 - Track Students

```http
GET /api/tracks/{id}/students
```

Returns students enrolled in a specific track.

The response includes enrollment-related information such as:

* Student
* Enrollment Status
* Enrollment Date
* Progress

---

# 💳 Payment Queries

## Query 12 - Unpaid Enrollments

```http
GET /api/reports/unpaid-enrollments
```

Returns enrollments where payment is missing or incomplete.

The response provides payment information and the remaining amount where applicable.

---

## Query 13 - Payments By Date Range

```http
GET /api/payments?from=2026-07-01&to=2026-07-31
```

Returns payments within the requested date range.

The API validates that:

```text
from <= to
```

### EF Core Concept

```text
Where
PaymentDate
Date Range Filtering
```

---

# 📊 Reporting Queries

## Query 14 - Revenue Summary

```http
GET /api/reports/revenue-summary
```

Returns financial summary information including:

* Total Revenue
* Paid Count
* Pending Count
* Failed Count

Money values are represented using `decimal`.

---

## Query 15 - Revenue Per Track

```http
GET /api/reports/revenue-by-track
```

Groups paid payments by training track.

The result contains:

* Track Title
* Total Paid Amount
* Enrollment Count

### EF Core Concept

```text
GroupBy
Sum
Count
Select
```

---

## Query 16 - Top Tracks By Enrollment

```http
GET /api/reports/top-tracks
```

Returns the tracks ordered by their active enrollment count.

The default result returns the top five tracks.

### EF Core Concepts

```text
GroupBy
Count
OrderByDescending
```

---

## Query 17 - Instructor Workload

```http
GET /api/reports/instructor-workload
```

Provides management information about instructor workload.

The report includes:

* Instructor
* Number of Tracks
* Number of Active Students

---

## Query 18 - Students Without Payments

```http
GET /api/reports/students-without-payments
```

Returns students who have active or pending enrollments without any payment.

Cancelled enrollments are excluded.

### EF Core Concept

```text
Any
All
Related Payments
```

---

# 🔎 Advanced Querying

## Query 19 - Advanced Enrollment Filter

```http
GET /api/enrollments?trackId=1&status=Active&paymentStatus=Paid
```

This query combines multiple optional filters.

Each filter is added only when its parameter has a value.

Example:

```text
trackId       → optional
status        → optional
paymentStatus → optional
studentId     → optional
```

This is implemented using conditional `IQueryable` composition.

### EF Core Concept

```text
IQueryable
Where
Conditional Filtering
```

---

## Query 20 - Dashboard Summary

```http
GET /api/reports/dashboard-summary
```

Returns important system statistics in one response.

The dashboard includes:

* Students Count
* Tracks Count
* Active Enrollments
* Revenue
* Unpaid Enrollments

The report combines multiple aggregate queries into a single business-oriented response.

---

# 📄 Pagination

Pagination is used to avoid returning large datasets in one request.

Example:

```http
GET /api/students?pageNumber=1&pageSize=10
```

The query uses:

```csharp
CountAsync()
Skip()
Take()
```

The API returns pagination metadata along with the result items.

Example structure:

```json
{
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 50,
  "totalPages": 5,
  "items": []
}
```

---

# 🎯 Projection

The query endpoints use DTO projection instead of returning complete EF Core entities.

Example:

```csharp
.Select(s => new StudentListItemResponse
{
    StudentId = s.StudentId,
    FullName = s.FullName,
    Email = s.Email,
    Phone = s.PhoneNumber,
    IsActive = s.IsActive
})
```

This approach returns only the fields required by the API response and avoids exposing unnecessary entity data.

---

# 🛡 Query Validation

Query parameters are validated before executing database queries.

Examples include:

* Invalid page number.
* Invalid page size.
* Invalid status.
* Invalid track level.
* Invalid date range.
* Invalid IDs.
* Unsupported filter values.

Invalid requests return clear error responses with appropriate HTTP status codes.

---

# 🧪 Testing

The query endpoints are tested using:

### Swagger

Swagger is used to:

* View endpoint documentation.
* Enter query parameters.
* Execute requests.
* Verify response DTOs.

### Postman

Postman is used to test different query scenarios, including:

* Valid searches.
* Filtering.
* Pagination.
* Combined filters.
* Invalid parameters.
* Empty results.
* Report endpoints.

---

# 📸 Evidence

Task evidence includes:

* Swagger screenshots for query endpoints.
* Postman query examples.
* Query responses.
* Report responses.
* Validation examples.

At least eight query endpoints are documented with Swagger screenshots as required by the task.

---

# 📖 Five Selected Query Explanations

The README highlights five important querying patterns:

### 1. Student Search

Uses `Where` and `Contains` to search by multiple student fields.

### 2. Student Pagination

Uses `CountAsync`, `Skip`, and `Take` to return paginated results with metadata.

### 3. Enrollment Filtering

Uses conditional `IQueryable` composition to apply only the filters provided by the client.

### 4. Revenue By Track

Uses `GroupBy`, `Sum`, and `Count` to convert payment data into business-level financial information.

### 5. Tracks With Available Seats

Uses enrollment counting and capacity comparison to identify tracks that still have available seats.

---

# 🚀 Running the Queries

Start the API:

```bash
dotnet run
```

Open Swagger:

```text
https://localhost:<port>/swagger
```

Example requests:

```text
GET /api/students?search=mohamed

GET /api/students?pageNumber=1&pageSize=10

GET /api/tracks?level=Beginner

GET /api/enrollments?status=Pending

GET /api/enrollments?trackId=1&status=Active&paymentStatus=Paid

GET /api/reports/revenue-summary

GET /api/reports/top-tracks

GET /api/reports/dashboard-summary
```

---

# 🏁 Task Status

### Completed ✅

The Task 04 query and reporting requirements are implemented using production-style EF Core querying patterns.

The implementation demonstrates:

* Searching
* Filtering
* Pagination
* Conditional queries
* DTO projection
* Related-data queries
* Aggregations
* Grouping
* Revenue calculations
* Business reports
* Query validation
* Swagger documentation
* Postman testing
