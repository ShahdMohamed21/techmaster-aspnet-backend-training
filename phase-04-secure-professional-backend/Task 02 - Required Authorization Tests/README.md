# Task 02 – Role Stories & Access Control

## Overview
This task implements role-based access control (RBAC) in the Training Center API using ASP.NET Core Authorization. The system separates permissions for Admin, Instructor, and Student to ensure that each user can access only the resources allowed for their role.

## User Roles

### 1. Admin
- Manage students and instructors.
- Create, update, assign, and manage training tracks.
- View all enrollments.
- Update payment statuses.
- Access all administrative reports.
- Manage audit logs.

### 2. Instructor
- View assigned training tracks only.
- View students enrolled in their own tracks.
- Create and update sessions for assigned tracks.
- View progress reports for their own tracks.
- Cannot access payment revenue reports.
- Cannot manage other instructors.

### 3. Student
- View and update their own profile.
- View their own enrollments and payment history.
- View available training tracks.
- Submit enrollment requests for themselves only.
- Cannot access other students' private information or administrative reports.

## Access Control Matrix

| Endpoint Group | Admin | Instructor | Student |
|---|---|---|---|
| Students | Full access | Read students in assigned tracks | Own profile only |
| Instructors | Full access | Own profile only | No access |
| Tracks | Full access | Assigned tracks with limited updates | View available tracks |
| Enrollments | Full access | Read enrollments in assigned tracks | Own enrollments only |
| Payments | Full access | No revenue access | Own payment history |
| Reports | All administrative reports | Own track reports only | No administrative reports |
| Audit Logs | Full access | Own operations (optional) | No access |

## Authorization Rules

- All protected endpoints require authentication.
- Role-based authorization restricts access according to the user's role.
- Ownership checks prevent instructors from accessing other instructors' tracks.
- Students can access only their own profile, enrollments, and payment history.
- Track management is Admin-owned.
- Payment status updates are restricted to Admin.
- Students cannot enroll another student or modify privileged account fields.
- Unauthorized requests return **401 Unauthorized** when authentication is missing or invalid, or **403 Forbidden** when an authenticated user lacks permission.

## Required Authorization Tests

| # | Request | Expected Result | Purpose |
|---|---|---|---|
| 1 | `GET /api/reports/revenue-summary` without a token | 401 Unauthorized | Reject unauthenticated requests |
| 2 | `GET /api/reports/revenue-summary` with Student token | 403 Forbidden | Prevent students from accessing admin reports |
| 3 | `GET /api/instructor/my-tracks` with Instructor token | 200 OK | Allow instructors to view their assigned tracks |
| 4 | `GET /api/tracks/{otherId}/students` with Instructor token | 403 Forbidden | Prevent access to another instructor's track |
| 5 | `GET /api/student/my-enrollments` with Student token | 200 OK | Allow students to view their own enrollments |
| 6 | `GET /api/students/{otherId}` with Student token | 403 Forbidden | Prevent access to another student's profile |
| 7 | `PUT /api/payments/{id}/status` with Admin token | 200 OK | Allow Admin to update payment status |
| 8 | `PUT /api/payments/{id}/status` with Student token | 403 Forbidden | Prevent students from updating payment status |

> **Note:** The expected results assume valid test data and a correctly configured authentication setup. A request for a missing resource may return `404 Not Found` instead of `403 Forbidden`, depending on the endpoint's implementation.

## Evidence and Testing

Authorization behavior is tested using Postman with separate tokens for Admin, Instructor, and Student.

The evidence includes:
- At least eight requests demonstrating successful access and denied access.
- HTTP status codes and response bodies.
- The role or collection variable used for each request, without exposing raw JWT tokens.
- Screenshots demonstrating that role and ownership restrictions work correctly.

## Technologies
- ASP.NET Core Web API
- JWT Authentication
- Role-Based Authorization
- Entity Framework Core
- SQL Server
- Postman

## Result
The API applies role-based and ownership-based access restrictions to protect student information, instructor resources, payments, and administrative reports.
