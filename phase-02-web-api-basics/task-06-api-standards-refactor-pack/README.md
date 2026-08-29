# Task 06 - API Standards & Refactor Pack

## Overview

This task focuses on refactoring a poorly designed ASP.NET Core Web API into a cleaner and more professional structure.

The original API had business logic, validation, and data storage directly inside the controller. It also used poor routes, public fields, and incorrect HTTP status codes.

The refactored version separates responsibilities using Controllers, DTOs, Models, and a Service layer.

---

## Original Problems

The original API had several design and implementation problems:

* The `Product` model used public fields instead of properties.
* The POST endpoint accepted parameters directly instead of using a request body DTO.
* Validation errors returned `200 OK` instead of appropriate error status codes.
* Product storage was directly inside the controller.
* Business logic was directly inside the controller.
* There was no service layer.
* Routes such as `all` and `get` were not RESTful.
* The API returned unclear error responses such as `"not found"`.
* The controller was responsible for too many things.
* There was no clear separation between request and response models.

---

## Improvements Made

### 1. Added Product Model with Properties

The original `Product` class used public fields.

The refactored version uses proper C# properties:

```csharp
public int Id { get; set; }
public string Name { get; set; }
public decimal Price { get; set; }
public int Stock { get; set; }
```

This follows standard C# and object-oriented design practices.

---

### 2. Added CreateProductRequest DTO

Instead of receiving multiple parameters directly in the POST endpoint, the API now accepts a request DTO.

Example:

```json
{
  "name": "Laptop",
  "price": 25000,
  "stock": 10
}
```

This makes the API request structure clearer and easier to maintain.

---

### 3. Added ProductResponse DTO

A separate response DTO was created to define the shape of the data returned to the client.

This provides a clear separation between the internal model and the API response.

---

### 4. Added Service Layer

Created:

* `IProductService`
* `ProductService`

The service layer is responsible for:

* Creating products.
* Validating product data.
* Storing products.
* Retrieving all products.
* Retrieving a product by ID.

This keeps business logic outside the controller.

---

### 5. Moved Validation to the Service

Validation was moved from the controller into `ProductService`.

The service checks:

* Product name is not empty.
* Product price is not negative.

Invalid data results in exceptions that are handled by the controller and returned as `400 Bad Request`.

---

### 6. Added AutoMapper

AutoMapper was introduced to handle mapping between:

```text
CreateProductRequest → Product
Product → ProductResponse
```

A `MappingProfile` was created to define these mappings.

This reduces repetitive manual mapping code.

---

### 7. Fixed HTTP Status Codes

The original API returned `200 OK` even when validation failed or a product was not found.

The refactored API uses appropriate HTTP status codes:

| Situation                       |       Status Code |
| ------------------------------- | ----------------: |
| Product created successfully    |     `201 Created` |
| Products retrieved successfully |          `200 OK` |
| Product retrieved successfully  |          `200 OK` |
| Invalid product data            | `400 Bad Request` |
| Product does not exist          |   `404 Not Found` |

---

### 8. Improved RESTful Routes

The original routes were:

```text
GET /api/products/all
GET /api/products/get?id=1
```

They were replaced with RESTful routes:

```text
POST /api/products
GET /api/products
GET /api/products/{id}
```

This makes the API easier to understand and follows REST API conventions.

---

### 9. Improved Error Response Shape

Instead of returning plain strings such as:

```text
not found
```

the API now returns a structured response:

```json
{
  "message": "Product Does Not Exist"
}
```

Validation errors also return a clear message:

```json
{
  "message": "Name Is Required!"
}
```

This provides a more consistent API response format.

---

### 10. Reduced Controller Responsibilities

The controller is now mainly responsible for handling HTTP requests and responses.

The controller delegates product operations to `IProductService`.

This results in a smaller, cleaner, and easier-to-maintain controller.

---

## Before vs After

| Before              | After                                |
| ------------------- | ------------------------------------ |
| Public fields       | Properties                           |
| Parameters in POST  | Request DTO                          |
| No response DTO     | `ProductResponse`                    |
| Logic in Controller | Logic in Service                     |
| No Service layer    | `IProductService` + `ProductService` |
| `200 OK` for errors | `400` / `404`                        |
| `/all` route        | `/api/products`                      |
| `/get?id=1` route   | `/api/products/{id}`                 |
| Plain error strings | Structured error responses           |
| Manual mapping      | AutoMapper                           |
| Large controller    | Smaller controller                   |

---

## API Endpoints

### Create Product

```http
POST /api/products
```

Request body:

```json
{
  "name": "Laptop",
  "price": 25000,
  "stock": 10
}
```

Expected response:

```text
201 Created
```

---

### Get All Products

```http
GET /api/products
```

Expected response:

```text
200 OK
```

---

### Get Product By ID

```http
GET /api/products/1
```

If the product exists:

```text
200 OK
```

If the product does not exist:

```text
404 Not Found
```

Example:

```json
{
  "message": "Product Does Not Exist"
}
```

---

## Project Structure

```text
task-06-api-standards-refactor-pack/
│
├── README.md
│
├── OriginalBadCode/
│   └── ProductsController.cs
│
└── RefactoredApi/
    ├── Controllers/
    │   └── ProductsController.cs
    │
    ├── Models/
    │   └── Product.cs
    │
    ├── DTOs/
    │   ├── CreateProductRequest.cs
    │   └── ProductResponse.cs
    │
    ├── Mapping/
    │   └── MappingProfile.cs
    │
    ├── Services/
    │   ├── IProductService.cs
    │   └── ProductService.cs
    │
    └── Program.cs
```

---

## What I Learned

Through this refactoring task, I learned how important separation of responsibilities is when building Web APIs. I learned how to keep controllers small and move business logic into a service layer. I also learned how DTOs can provide clear request and response structures without exposing internal models directly. Using proper HTTP status codes makes API behavior more predictable for clients. I also practiced RESTful routing and structured error responses. Finally, I learned how AutoMapper can simplify mapping between models and DTOs and reduce repetitive code.
