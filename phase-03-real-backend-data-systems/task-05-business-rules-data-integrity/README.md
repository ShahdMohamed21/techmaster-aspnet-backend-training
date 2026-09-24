# Task 05 - Business Rules & Data Integrity

## Overview

This task focuses on implementing business rules inside the service layer to prevent invalid data and invalid operations.

The API validates business conditions before modifying the database and returns clear `400 Bad Request` responses when a rule is violated.

---

## Business Rules

### Student Rules

* Email must be unique.
* FullName is required.
* Students are soft deleted using `IsDeleted`.
* Deleted students do not appear in normal list endpoints.
* Inactive or deleted students cannot create new active enrollments.

### Track Rules

* Title is required.
* Track Code must be unique.
* Capacity must be greater than `0`.
* StartDate must be before EndDate.
* Instructor is required.
* A track cannot exceed its capacity.
* Closed tracks cannot accept new enrollments.

### Enrollment Rules

* A student cannot have two active enrollments in the same track.
* New enrollments start with `Pending` status.
* Enrollment can become `Active` according to the payment/status rules.
* Completed enrollments cannot be cancelled directly.
* Cancelled enrollments do not count toward track capacity.

### Payment Rules

* Payment amount must be greater than `0`.
* Payment cannot exceed the remaining amount.
* Payment status can be `Pending`, `Paid`, `Failed`, or `Refunded`.
* Only `Paid` payments are included in revenue reports.
* Failed payments do not activate enrollments.

---

## Validation & Error Handling

Business rules are implemented inside the service layer.

Invalid operations return:

```json
{
  "success": false,
  "message": "Clear validation message"
}
```

with HTTP status:

`400 Bad Request`

---

## Testing

The business rules are tested using Swagger and Postman.

### Invalid Scenarios

* Duplicate student email → `400`
* Track capacity `0` → `400`
* Duplicate track code → `400`
* Enroll in a full track → `400`
* Enroll a student twice in the same track → `400`
* Payment amount `0` → `400`
* Payment above remaining amount → `400`

### Valid Scenarios

* Create valid student
* Create valid track
* Create valid enrollment
* Create valid payment

---

## Evidence

The task evidence includes:

* Successful valid requests.
* Failed requests showing `400 Bad Request`.
* Deleted students excluded from normal lists.
* Duplicate enrollment validation.
* Track capacity validation.
* Payment validation.
* Postman test results.
* Swagger screenshots.

---

## Implementation

The rules are implemented mainly in:

```text
Services/
├── StudentService.cs
├── TrackService.cs
├── EnrollmentService.cs
└── PaymentService.cs
```

This keeps business logic inside the service layer instead of putting it directly in controllers.

---

## Commit

Example commit:

```text
Implement business rules and data integrity validations
```
