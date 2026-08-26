# Task 02 - Student Management API

## Overview

This project is an in-memory Student Management API built with ASP.NET Core Web API.

The API simulates a training center that manages students and their training tracks before moving the data to a real database.

The project demonstrates:

- Controller-based Web API
- DTOs for requests and responses
- Service layer for business logic
- In-memory storage using `List<T>`
- CRUD operations
- Search and filtering
- Pagination
- Validation
- Proper HTTP status codes
- Student statistics

---

## Technologies Used

- ASP.NET Core Web API
- C#
- .NET
- Swagger / OpenAPI
- Postman
- In-memory storage using `List<T>`

---

## Project Structure

```text
task-02-student-management-api/
│
├── README.md
│
└── StudentManagementApi/
    │
    ├── Controllers/
    │   └── StudentsController.cs
    │
    ├── Models/
    │   └── Student.cs
    │
    ├── DTOs/
    │   ├── CreateStudentRequest.cs
    │   ├── UpdateStudentRequest.cs
    │   ├── UpdateStudentStatusRequest.cs
    │   ├── StudentResponse.cs
    │   └── StudentStatsResponse.cs
    │
    ├── Services/
    │   ├── IStudentService.cs
    │   └── StudentService.cs
    │
    └── Program.cs
Student Model

The Student model contains the following fields:

Field	Description
StudentId	Unique student identifier
FullName	Student full name
Email	Student email address
PhoneNumber	Student phone number
TrackName	Training track
EnrollmentDate	Student enrollment date
IsActive	Student active status
GitHubProfileUrl	Optional GitHub profile
LinkedInProfileUrl	Optional LinkedIn profile
DTOs
CreateStudentRequest

Used when creating a new student.

Required fields:

FullName
Email
PhoneNumber
TrackName
EnrollmentDate
IsActive

Optional fields:

GitHubProfileUrl
LinkedInProfileUrl

Email validation is applied using the EmailAddress attribute.

UpdateStudentRequest

Used to update the student's core information.

The StudentId is not included because the student ID must not be changed.

UpdateStudentStatusRequest

Used to activate or deactivate a student.

Example:

{
  "isActive": false
}
StudentResponse

Used to return student information from the API instead of exposing the internal model directly.

StudentStatsResponse

Used to return student statistics including:

Total students
Active students
Inactive students
Number of students by track
API Endpoints
1. Create Student
Request
POST /api/students
Example Body
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "phoneNumber": "01012345678",
  "trackName": ".NET",
  "enrollmentDate": "2026-08-27",
  "isActive": true,
  "gitHubProfileUrl": "https://github.com/johndoe",
  "linkedInProfileUrl": "https://linkedin.com/in/johndoe"
}
Response
201 Created

The API returns the created student.

The email must be unique.

2. Get All Students
Request
GET /api/students

Returns all students.

Search

Search by student name or email:

GET /api/students?search=John
Filter by Track
GET /api/students?trackName=.NET
Filter by Active Status
GET /api/students?isActive=true
Pagination
GET /api/students?pageNumber=1&pageSize=10
Combine Options

Search, filtering, and pagination can be combined:

GET /api/students?search=John&trackName=.NET&isActive=true&pageNumber=1&pageSize=5
Response
200 OK
3. Get Student By ID
Request
GET /api/students/{id}
Example
GET /api/students/1
Responses

If the student exists:

200 OK

If the student does not exist:

404 Not Found

The API does not return null with a 200 OK response.

4. Update Student
Request
PUT /api/students/{id}
Example
PUT /api/students/1
Example Body
{
  "fullName": "John Doe Updated",
  "email": "john.updated@example.com",
  "phoneNumber": "01012345678",
  "trackName": ".NET",
  "enrollmentDate": "2026-08-27",
  "gitHubProfileUrl": "https://github.com/johndoe",
  "linkedInProfileUrl": "https://linkedin.com/in/johndoe"
}
Response
200 OK

The updated student is returned.

Business Rules
StudentId cannot be changed.
Required fields are validated.
Returns 404 Not Found if the student does not exist.
5. Update Student Status
Request
PATCH /api/students/{id}/status
Example
PATCH /api/students/1/status
Example Body
{
  "isActive": false
}
Response
200 OK

Example response:

{
  "message": "Student deactivated successfully",
  "student": {
    "studentId": 1,
    "fullName": "John Doe",
    "isActive": false
  }
}

This operation changes the student's active status without deleting the student history.

If the student does not exist:

404 Not Found
6. Get Student Statistics
Request
GET /api/students/stats
Response
200 OK
Example Response
{
  "totalStudents": 5,
  "activeStudents": 3,
  "inactiveStudents": 2,
  "studentsByTrack": {
    ".NET": 3,
    "Flutter": 2
  }
}
Validation

The API uses Data Annotations for request validation.

Examples:

[Required]
[EmailAddress]

Invalid create or update requests return:

400 Bad Request
HTTP Status Codes
Status Code	Usage
200 OK	Successful GET, UPDATE, or status change
201 Created	Student successfully created
400 Bad Request	Invalid request or validation failure
404 Not Found	Student does not exist
In-Memory Storage

The project uses:

List<Student>

as temporary in-memory storage.

The application also contains seed students for testing.

No external database is used in this phase.

Business Logic

Business logic is implemented inside the StudentService.

The controller is responsible for:

Receiving HTTP requests
Calling the service
Returning appropriate HTTP responses

The service is responsible for:

Managing students
Creating students
Searching and filtering students
Updating students
Updating student status
Generating statistics
Testing

The API was tested using:

Swagger UI
Postman

Evidence includes screenshots for:

Create Student
Get All Students
Get Student By ID
Update Student
Update Student Status
Student Statistics
How to Run
Open the project in Visual Studio.
Build the solution.
Run the application.
Open Swagger UI.
Test the endpoints using Swagger or Postman.

Example Swagger URL:

https://localhost:<port>/swagger
Task Acceptance Criteria
 Controller-based API
 All required DTOs implemented
 Service layer implemented
 In-memory storage using List<T>
 Create student
 Get all students
 Get student by ID
 Update student
 Update student status
 Search implemented
 Filtering implemented
 Pagination implemented
 Student statistics implemented
 Validation implemented
 Correct HTTP status codes
 Missing students return 404
 README documentation added
 Swagger testing completed
 Postman testing completed
