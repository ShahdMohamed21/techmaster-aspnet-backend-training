# Task 01 - REST & Routing Drills

This task contains small ASP.NET Core Web API drills focused on controllers, routing, query strings, JSON responses, services, and dependency injection.

## Completed Drills

| Drill No. | Endpoint                                            | Concept          | Status | Evidence |
| --------- | --------------------------------------------------- | ---------------- | ------ | -------- |
| 01        | `GET /api/health`                                   | Basic endpoint   | Done   | Swagger  |
| 02        | `GET /api/tools/echo/{name}`                        | Route parameter  | Done   | Swagger  |
| 03        | `GET /api/calculator/add?a=10&b=5`                  | Query parameters | Done   | Swagger  |
| 04        | `GET /api/converter/celsius-to-fahrenheit?value=25` | Service + DI     | Done   | Swagger  |

## Drill Details

### Drill 01 - Health Check

**Purpose:** Check that the API is running and reachable.

**Sample Request:**

```http
GET /api/health
```

**Response:** `200 OK` with JSON containing:

* `status`
* `service`
* `time`

---

### Drill 02 - Route Parameter Echo

**Purpose:** Practice receiving a value from the route.

**Sample Request:**

```http
GET /api/tools/echo/Mohamed
```

**Response:** `200 OK`

```json
{
  "originalName": "Mohamed",
  "message": "Welcome Mohamed"
}
```

---

### Drill 03 - Query String Calculator

**Purpose:** Practice receiving values from the query string and performing a calculation.

**Sample Request:**

```http
GET /api/calculator/add?a=10&b=5
```

**Response:** `200 OK`

```json
{
  "a": 10,
  "b": 5,
  "operation": "addition",
  "result": 15
}
```

---

### Drill 04 - Temperature Conversion

**Purpose:** Convert Celsius to Fahrenheit using a separate service.

**Sample Request:**

```http
GET /api/converter/celsius-to-fahrenheit?value=25
```

**Response:** `200 OK`

```json
{
  "celsius": 25,
  "fahrenheit": 77,
  "formulaUsed": "(C × 9/5) + 32"
}
```

**Concepts practiced:**

* Query parameters
* Business logic in a service
* Dependency Injection
* JSON response

## Evidence

Screenshots for the completed drills are stored in the `Screenshots` folder.

### Progress

* [x] Drill 01 - Health Check
* [x] Drill 02 - Route Parameter Echo
* [x] Drill 03 - Query String Calculator
* [x] Drill 04 - Temperature Conversion
* [ ] Drills 05-15
