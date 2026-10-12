# Task 07 – Audit Trail & Activity Timeline

## Overview
Implemented an Audit Trail system for the Training Center API to track important user and system activities. The system records actions, timestamps, affected entities, user information, IP addresses, and metadata to improve traceability and accountability.

## Features
- **User Activity Tracking:** Logs successful user registration and login.
- **Track Management:** Records training track creation.
- **Enrollment Tracking:** Logs enrollment requests and status changes.
- **Payment Tracking:** Records payment creation and payment status updates.
- **Audit Metadata:** Stores user ID, user role, action, entity name, entity ID, description, timestamp, IP address, and JSON metadata.
- **Request Identification:** Includes request ID metadata for tracing activities.
- **Admin-Only Access:** Activity logs are intended to be accessible only to authorized administrators.
- **Filtering:** Supports filtering logs by user ID, entity name, and date range.
- **Pagination:** Supports paginated audit log retrieval.

## Logged Events

| Event | Description |
|---|---|
| `UserRegistered` | A new user registers successfully. |
| `UserLoggedIn` | A user logs in successfully. |
| `TrackCreated` | A training track is created. |
| `EnrollmentRequested` | A student requests enrollment. |
| `EnrollmentStatusUpdated` | An enrollment status changes. |
| `PaymentCreated` | A payment record is created. |
| `PaymentStatusUpdated` | A payment status changes. |

## API Endpoint

### Get Activity Logs
`GET /api/admin/activity-logs`

**Supported query parameters:**
- `userId` – Filter logs by user.
- `entityName` – Filter by entity type.
- `from` – Filter logs from a specific date.
- `to` – Filter logs up to a specific date.
- `pageNumber` – Specify the page number.
- `pageSize` – Specify the number of records per page.

**Example:**

`GET /api/admin/activity-logs?entityName=Payment&pageNumber=1&pageSize=10`

**Authorization:** Admin role required.

## Technical Implementation
- ASP.NET Core Web API
- Entity Framework Core
- SQL database
- Dependency Injection
- Service-based architecture
- JSON serialization for audit metadata
- JWT authentication and role-based authorization

## Purpose
The Audit Trail provides a centralized record of important system activities, making it easier to investigate changes, monitor user actions, and maintain accountability across the Training Center platform.
