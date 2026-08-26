# Task 01 - REST & Routing Drills

This task contains 15 small ASP.NET Core Web API drills focused on controllers, routing, route parameters, query strings, request bodies, headers, JSON responses, HTTP status codes, services, dependency injection, validation, CRUD operations, pagination, and standard error responses.

## Completed Drills

| Drill No. | Endpoint | Concept | Status | Evidence |
|---|---|---|---|---|
| 01 | `GET /api/health` | Basic endpoint | Done | Swagger |
| 02 | `GET /api/tools/echo/{name}` | Route parameter | Done | Swagger |
| 03 | `GET /api/calculator/add?a=10&b=5` | Query parameters | Done | Swagger |
| 04 | `GET /api/converter/celsius-to-fahrenheit?value=25` | Service + DI | Done | Swagger |
| 05 | `GET /api/grades/calculate?score=85` | Validation + conditions | Done | Swagger |
| 06 | `POST /api/notes` | Request body + DTO | Done | Swagger/Postman |
| 07 | `GET /api/notes` | Collection response | Done | Swagger/Postman |
| 08 | `GET /api/notes/{id}` | Route parameter + 404 | Done | Swagger/Postman |
| 09 | `PUT /api/notes/{id}` | PUT update | Done | Swagger/Postman |
| 10 | `DELETE /api/notes/{id}` | DELETE + status codes | Done | Swagger/Postman |
| 11 | `GET /api/notes/search?keyword=api` | Search query | Done | Swagger/Postman |
| 12 | `GET /api/notes?pageNumber=1&pageSize=5` | Pagination | Done | Swagger/Postman |
| 13 | `GET /api/request-info` | Request headers | Done | Postman |
| 14 | `GET /api/statuscodes/{id}` | HTTP status codes | Done | Swagger/Postman |
| 15 | `GET /api/errors/demo?type=bad-request` | Standard error shape | Done | Swagger/Postman |

---

# Drill Details

## Drill 01 - Health Check

**Purpose:** Check that the API is running and reachable.

**Endpoint:**

```http
GET /api/health

Response: 200 OK

{
  "status": "Running",
  "service": "TechMaster API",
  "time": "2026-08-26T00:00:00Z"
}

Concepts practiced:

Basic controller
HTTP GET
HTTP 200 OK
JSON response
Server time
Drill 02 - Route Parameter Echo

Purpose: Practice receiving a value from the route.

Endpoint:

GET /api/tools/echo/{name}

Sample Request:

GET /api/tools/echo/Mohamed

Response: 200 OK

{
  "originalName": "Mohamed",
  "message": "Welcome Mohamed"
}

Empty or whitespace names are rejected with 400 Bad Request.

Concepts practiced:

Route parameters
Controller actions
BadRequest()
Ok()
Drill 03 - Query String Calculator

Purpose: Practice receiving values from the query string and performing a calculation.

Endpoint:

GET /api/calculator/add?a=10&b=5

Response: 200 OK

{
  "a": 10,
  "b": 5,
  "operation": "addition",
  "result": 15
}

Concepts practiced:

Query parameters
[FromQuery]
Decimal values
JSON responses
Basic calculations
Drill 04 - Temperature Conversion

Purpose: Convert Celsius to Fahrenheit using a separate service.

Endpoint:

GET /api/converter/celsius-to-fahrenheit?value=25

Response: 200 OK

{
  "celsius": 25,
  "fahrenheit": 77,
  "formulaUsed": "(C × 9/5) + 32"
}

Formula:

F = (C × 9 / 5) + 32

Concepts practiced:

Query parameters
Business logic
Services
Dependency Injection
JSON responses
Drill 05 - Grade API

Purpose: Validate a score and calculate the grade and pass/fail status.

Endpoint:

GET /api/grades/calculate?score=85

Response: 200 OK

{
  "score": 85,
  "status": "Pass",
  "grade": "B"
}

Scores outside the range 0-100 return:

400 Bad Request

Concepts practiced:

Query parameters
Input validation
Conditions
HTTP 400 Bad Request
Grade calculation
Drill 06 - Create Note

Purpose: Practice receiving JSON data through a POST request using a DTO.

Endpoint:

POST /api/notes

Request Body:

{
  "title": "Learning API",
  "content": "ASP.NET Core Web API"
}

Response: 201 Created

{
  "id": 1,
  "title": "Learning API",
  "content": "ASP.NET Core Web API",
  "createdAt": "2026-08-26T00:00:00Z"
}

Notes are stored in a static in-memory list for this drill.

Concepts practiced:

HTTP POST
Request body
DTO
[FromBody]
Data validation
201 Created
In-memory storage
Drill 07 - Get Notes List

Purpose: Return the collection of notes stored in memory.

Endpoint:

GET /api/notes

Response: 200 OK

[
  {
    "id": 1,
    "title": "Learning API",
    "content": "ASP.NET Core Web API",
    "createdAt": "2026-08-26T00:00:00Z"
  }
]

If there are no notes, an empty array is returned:

[]

Concepts practiced:

HTTP GET
Collection responses
JSON arrays
In-memory data
Drill 08 - Get Note By ID

Purpose: Retrieve one note using its route ID.

Endpoint:

GET /api/notes/{id}

Sample Request:

GET /api/notes/1

Response: 200 OK when the note exists.

If the note does not exist:

404 Not Found

Example:

{
  "message": "Note not found"
}

Concepts practiced:

Route parameters
FirstOrDefault()
Finding resources
HTTP 404 Not Found
Drill 09 - Update Note

Purpose: Practice updating an existing resource using PUT.

Endpoint:

PUT /api/notes/{id}

Sample Request:

PUT /api/notes/1

Request Body:

{
  "title": "Updated Note",
  "content": "Updated content"
}

Response: 200 OK

The existing note is updated instead of creating a new note.

If the note does not exist:

404 Not Found

If the title or content is empty:

400 Bad Request

Concepts practiced:

HTTP PUT
Route parameters
Request body
DTOs
Updating existing resources
Validation
Drill 10 - Delete Note

Purpose: Practice deleting a resource using the DELETE HTTP method.

Endpoint:

DELETE /api/notes/{id}

Sample Request:

DELETE /api/notes/1

Response:

204 No Content

If the note does not exist:

404 Not Found

Concepts practiced:

HTTP DELETE
Route parameters
Removing resources
NoContent()
HTTP 404 Not Found
Drill 11 - Search Notes

Purpose: Search notes by title or content using a query parameter.

Endpoint:

GET /api/notes/search?keyword=api

The search is case-insensitive and checks both the title and content.

Response: 200 OK

[
  {
    "id": 1,
    "title": "Learning API",
    "content": "ASP.NET Core Web API"
  }
]

If the keyword is empty:

400 Bad Request

If there are no matching notes, an empty array is returned:

[]

Concepts practiced:

Query parameters
String searching
Contains()
StringComparison.OrdinalIgnoreCase
LINQ
Validation
Drill 12 - Pagination

Purpose: Practice API pagination using Skip() and Take().

Endpoint:

GET /api/notes?pageNumber=1&pageSize=5

Response: 200 OK

{
  "items": [],
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 10
}

Pagination uses:

Skip = (pageNumber - 1) * pageSize

Validation rules:

pageNumber must be greater than 0.
pageSize must be between 1 and 50.

Invalid values return:

400 Bad Request

Concepts practiced:

Query parameters
Pagination
Skip()
Take()
LINQ
Validation
Drill 13 - Request Header Reader

Purpose: Practice reading a custom HTTP request header.

Endpoint:

GET /api/request-info

Request Header:

X-Student-Name: Shahd

Response: 200 OK

{
  "studentName": "Shahd",
  "path": "/api/request-info"
}

If the header is missing:

400 Bad Request

The student name is read from the request header and is not hardcoded.

Concepts practiced:

HTTP headers
Request.Headers
Custom headers
Request path
HTTP 400 Bad Request
Drill 14 - Status Code Practice

Purpose: Practice common HTTP status codes through different API operations.

Get Existing Resource

Endpoint:

GET /api/statuscodes/100

Response: 200 OK

{
  "id": 100,
  "name": "Sample Note"
}
Get Missing Resource

Request:

GET /api/statuscodes/999

Response:

404 Not Found
Create Resource

Endpoint:

POST /api/statuscodes

Request Body:

{
  "title": "Test Note",
  "content": "Testing status codes"
}

Response:

201 Created
Delete Resource

Request:

DELETE /api/statuscodes/100

Response:

204 No Content
Validate Request

Endpoint:

POST /api/statuscodes/validate

Request Body:

{
  "title": "",
  "content": "Test"
}

Response:

400 Bad Request

Concepts practiced:

200 OK
201 Created
204 No Content
400 Bad Request
404 Not Found
REST status codes
Drill 15 - Standard Error Shape

Purpose: Practice returning a consistent JSON structure for API errors.

Endpoint:

GET /api/errors/demo
Bad Request Example

Sample Request:

GET /api/errors/demo?type=bad-request

Response: 400 Bad Request

{
  "message": "Invalid request",
  "code": "BAD_REQUEST",
  "details": [
    "The request data is invalid"
  ]
}
Not Found Example

Sample Request:

GET /api/errors/demo?type=not-found

Response: 404 Not Found

{
  "message": "Resource not found",
  "code": "NOT_FOUND",
  "details": [
    "The requested resource does not exist"
  ]
}
Invalid Error Type

Sample Request:

GET /api/errors/demo?type=unknown

Response:

400 Bad Request

The error response follows a consistent structure containing:

message
code
details

Concepts practiced:

Standard error responses
HTTP 400
HTTP 404
JSON response shapes
Consistent API error structure
Concepts Practiced

Throughout the 15 drills, the following Web API concepts were practiced:

Controllers
Controller actions
HTTP methods
GET
POST
PUT
DELETE
Route parameters
Query parameters
Request bodies
DTOs
Request headers
JSON responses
HTTP status codes
200 OK
201 Created
204 No Content
400 Bad Request
404 Not Found
Input validation
LINQ
FirstOrDefault()
Where()
Skip()
Take()
Services
Dependency Injection
In-memory data
Pagination
Standard error responses
Evidence

Screenshots for the completed drills are stored in the Screenshots folder.

Swagger was used to test and document the API endpoints.

Postman was used for API testing and request/response verification.

Progress
 Drill 01 - Health Check
 Drill 02 - Route Parameter Echo
 Drill 03 - Query String Calculator
 Drill 04 - Temperature Conversion
 Drill 05 - Grade API
 Drill 06 - Create Note
 Drill 07 - Get Notes List
 Drill 08 - Get Note By ID
 Drill 09 - Update Note
 Drill 10 - Delete Note
 Drill 11 - Search Notes
 Drill 12 - Pagination
 Drill 13 - Request Header Reader
 Drill 14 - Status Code Practice
 Drill 15 - Standard Error Shape
Project Structure
task-01-rest-routing-drills/
│
├── README.md
│
└── Drills/
    ├── Controllers/
    │   ├── HealthController.cs
    │   ├── ToolsController.cs
    │   ├── CalculatorController.cs
    │   ├── ConverterController.cs
    │   ├── GradesController.cs
    │   ├── NotesController.cs
    │   ├── RequestInfoController.cs
    │   ├── StatusCodesController.cs
    │   └── ErrorsController.cs
    │
    ├── DTOs/
    │   ├── CreateNoteRequest.cs
    │   └── UpdateNoteRequest.cs
    │
    ├── Services/
    │   └── ConverterService.cs
    │
    └── Program.cs
Conclusion

Task 01 provides practical experience with the fundamentals of ASP.NET Core Web API and RESTful API design.

The drills progress from simple GET endpoints to request bodies, CRUD operations, validation, pagination, request headers, HTTP status codes, and standardized error responses.
