# BookStore API

A RESTful ASP.NET Core Web API for managing authors, categories, books, searching and filtering books, and generating management reports.

## Technologies

* ASP.NET Core Web API
* C#
* AutoMapper
* Swagger / OpenAPI
* In-Memory Collections
* RESTful API principles

---

# Project Structure

```text
BookStoreApi
│
├── Controllers
│   ├── AuthorsController.cs
│   ├── CategoriesController.cs
│   ├── BooksController.cs
│   └── ReportsController.cs
│
├── DTOs
│   ├── Authors
│   ├── Categories
│   ├── Books
│   
│
├── Models
│   ├── Author.cs
│   ├── Category.cs
│   └── Book.cs
│
├── Services
│   ├── IAuthorService.cs
│   ├── AuthorService.cs
│   ├── ICategoryService.cs
│   ├── CategoryService.cs
│   ├── IBookService.cs
│   ├── BookService.cs
│   └── ReportService.cs
│
├── Mapping
│   └── MappingProfile.cs
│
└── Program.cs
```

---

# Features

## Feature 01 - Authors API

Manage authors that books can be assigned to.

### Business Rules

* Author full name is required.
* Author ID must be unique.
* Deleting an author with books is blocked and returns a clear error.

### Endpoints

| Method | Endpoint            | Description         |
| ------ | ------------------- | ------------------- |
| POST   | `/api/Authors`      | Create a new author |
| GET    | `/api/Authors`      | Get all authors     |
| GET    | `/api/Authors/{id}` | Get author by ID    |
| PUT    | `/api/Authors/{id}` | Update an author    |
| DELETE | `/api/Authors/{id}` | Delete an author    |

### Example - Create Author

```json
{
  "fullName": "Malak Ahmed",
  "country": "Egypt",
  "birthDate": "1998-03-02"
}
```

### Validation

* Full name cannot be empty.
* Author ID is generated automatically.
* An author cannot be deleted if books are assigned to that author.

### Status Codes

* `201 Created` - Author created successfully.
* `200 OK` - Author retrieved or updated successfully.
* `400 Bad Request` - Invalid request.
* `404 Not Found` - Author does not exist.
* `409 Conflict` - Author cannot be deleted because books are assigned to the author.

### Review Questions

* **Why did you use this route?**
  I used `/api/Authors` because it follows RESTful routing and represents the authors resource.

* **What DTOs did you create?**
  I created `CreateAuthorRequest`, `UpdateAuthorRequest`, and `AuthorResponse` DTOs.

* **What validation rules did you apply?**
  I validated that the author full name is required, the ID is unique, and authors with books cannot be deleted.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with an EF Core database and repository/DbContext operations.

---

# Feature 02 - Categories API

Manage book categories.

### Business Rules

* Category name is required.
* Category name should be unique.
* Inactive categories cannot be used for new books.

### Endpoints

| Method | Endpoint               | Description        |
| ------ | ---------------------- | ------------------ |
| POST   | `/api/Categories`      | Create a category  |
| GET    | `/api/Categories`      | Get all categories |
| GET    | `/api/Categories/{id}` | Get category by ID |
| PUT    | `/api/Categories/{id}` | Update a category  |
| DELETE | `/api/Categories/{id}` | Delete a category  |

### Example - Create Category

```json
{
  "name": "Programming",
  "description": "Programming and software development books",
  "isActive": true
}
```

### Validation

* Category name cannot be empty.
* Category name must be unique.
* Inactive categories cannot be assigned to new books.

### Status Codes

* `201 Created` - Category created successfully.
* `200 OK` - Category retrieved or updated successfully.
* `400 Bad Request` - Invalid request.
* `404 Not Found` - Category does not exist.
* `409 Conflict` - Category name already exists or category cannot be deleted.

### Review Questions

* **Why did you use this route?**
  I used `/api/Categories` because it follows RESTful routing and represents the categories resource.

* **What DTOs did you create?**
  I created `CreateCategoryRequest`, `UpdateCategoryRequest`, and `CategoryResponse` DTOs.

* **What validation rules did you apply?**
  I validated that the category name is required and unique, and inactive categories cannot be assigned to new books.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with an EF Core database and database queries through `DbContext`.

---

# Feature 03 - Books API

Manage book records.

### Business Rules

* Title is required.
* ISBN is required and must be unique.
* Price must be positive.
* Stock cannot be negative.
* Author ID must exist.
* Category ID must exist.
* Inactive categories cannot be used for new books.
* Book availability is calculated from stock quantity.

### Availability Rule

```csharp
book.IsAvailable = book.StockQuantity > 0;
```

If `StockQuantity` is greater than zero, the book is available.

If `StockQuantity` is zero, the book is out of stock.

### Endpoints

| Method | Endpoint          | Description    |
| ------ | ----------------- | -------------- |
| POST   | `/api/Books`      | Create a book  |
| GET    | `/api/Books`      | Get all books  |
| GET    | `/api/Books/{id}` | Get book by ID |
| PUT    | `/api/Books/{id}` | Update a book  |
| DELETE | `/api/Books/{id}` | Delete a book  |

### Example - Create Book

```json
{
  "title": "ASP.NET Core Fundamentals",
  "isbn": "978-1234567890",
  "publishedYear": 2025,
  "price": 500,
  "stockQuantity": 10,
  "authorId": 1,
  "categoryId": 1
}
```

### Validation

* Title is required.
* ISBN is required.
* ISBN must be unique.
* Price must be greater than zero.
* Stock quantity cannot be negative.
* Author must exist.
* Category must exist.
* Category must be active.

### Status Codes

* `201 Created` - Book created successfully.
* `200 OK` - Book retrieved, updated, or deleted successfully.
* `400 Bad Request` - Invalid book data.
* `404 Not Found` - Book, author, or category does not exist.

### Review Questions

* **Why did you use this route?**
  I used `/api/Books` because it follows RESTful routing and represents the books resource.

* **What DTOs did you create?**
  I created `CreateBookRequest`, `UpdateBookRequest`, and `BookResponse` DTOs.

* **What validation rules did you apply?**
  I validated title, ISBN uniqueness, positive price, non-negative stock, and existing author and category IDs.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with EF Core entities, relationships, and database queries.

---

# Feature 04 - Search and Pagination

Allow searching and browsing books.

### Business Rules

* Search by title or ISBN.
* Filter by category.
* Filter by author.
* Filter by availability.
* Support pagination using `pageNumber` and `pageSize`.

### Endpoint

```http
GET /api/Books/search
```

### Query Parameters

| Parameter     | Type   | Description              |
| ------------- | ------ | ------------------------ |
| `search`      | string | Search by title or ISBN  |
| `categoryId`  | int    | Filter by category       |
| `authorId`    | int    | Filter by author         |
| `isAvailable` | bool   | Filter by availability   |
| `pageNumber`  | int    | Page number              |
| `pageSize`    | int    | Number of books per page |

### Example Requests

Search by title:

```http
GET /api/Books/search?search=Clean
```

Search by ISBN:

```http
GET /api/Books/search?search=978-0132350884
```

Filter by category:

```http
GET /api/Books/search?categoryId=1
```

Filter by author:

```http
GET /api/Books/search?authorId=1
```

Filter by availability:

```http
GET /api/Books/search?isAvailable=true
```

Pagination:

```http
GET /api/Books/search?pageNumber=1&pageSize=2
```

Combined search:

```http
GET /api/Books/search?search=Code&categoryId=1&isAvailable=true&pageNumber=1&pageSize=5
```

### DTO

```csharp
public class BookSearchRequest
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? AuthorId { get; set; }
    public bool? IsAvailable { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
```

### Status Codes

* `200 OK` - Search completed successfully.
* `400 Bad Request` - Invalid pagination parameters.

### Review Questions

* **Why did you use this route?**
  I used `/api/Books/search` because searching and filtering are operations performed on the books resource.

* **What DTOs did you create?**
  I created `BookSearchRequest` to hold search, filter, and pagination parameters.

* **What validation rules did you apply?**
  I validated the search and filter parameters and used `pageNumber` and `pageSize` for pagination.

* **How will this change when EF Core is added?**
  Filtering, searching, and pagination will be performed directly in the database using LINQ and EF Core.

---

# Feature 05 - Reports Summary

Generate a simple management summary.

### Business Rules

The report provides:

* Total books.
* Available books.
* Out-of-stock books.
* Books per category.
* Books per author.
* Total inventory value.

### Endpoint

```http
GET /api/Reports
```

### Report Response

Example:

```json
{
  "totalBooks": 4,
  "availableBooks": 3,
  "outOfStockBooks": 1,
  "booksPerCategory": {
    "Programming": 2,
    "Science": 1,
    "History": 1
  },
  "booksPerAuthor": {
    "Shahd Mohamed": 2,
    "Ahmed Mostafa": 1,
    "Mona Yasser": 1
  },
  "totalInventoryValue": 7800
}
```

### Inventory Value

The total inventory value is calculated using:

```text
Price × StockQuantity
```

for all books.

### Status Codes

* `200 OK` - Report generated successfully.

### Review Questions

* **Why did you use this route?**
  I used `/api/Reports` because it represents a management summary resource.

* **What DTOs did you create?**
  I created `ReportSummaryResponse` to return the required management statistics.

* **What validation rules did you apply?**
  No complex input validation is required because the report endpoint does not receive user input.

* **How will this change when EF Core is added?**
  The statistics will be calculated using database queries and LINQ instead of the in-memory books list.

---

# AutoMapper

AutoMapper is used to map between Models and DTOs.

### Mapping Profile

```csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorResponse>();
        CreateMap<CreateAuthorRequest, Author>();
        CreateMap<UpdateAuthorRequest, Author>();

        CreateMap<Category, CategoryResponse>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<Book, BookResponse>();
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
    }
}
```

AutoMapper keeps mapping logic out of the controllers and services and makes the code cleaner and easier to maintain.

---

# Error Handling

The API uses appropriate HTTP status codes for different scenarios.

| Status Code       | Meaning                                                |
| ----------------- | ------------------------------------------------------ |
| `200 OK`          | Request completed successfully                         |
| `201 Created`     | Resource created successfully                          |
| `400 Bad Request` | Invalid request or validation error                    |
| `404 Not Found`   | Requested resource does not exist                      |
| `409 Conflict`    | Request conflicts with existing data or business rules |

Example:

```csharp
catch (ArgumentException ex)
{
    return BadRequest(ex.Message);
}
```

For conflicts:

```csharp
return Conflict(ex.Message);
```

`409 Conflict` is used when the request is valid but cannot be completed because it conflicts with the current state of the data.

---

# Data Storage

The current implementation uses in-memory `List<T>` collections as temporary data storage.

Example:

```csharp
private readonly List<Book> books = new List<Book>();
```

This allows the API to demonstrate CRUD operations and business rules without requiring a database.

---

# Future EF Core Changes

When EF Core is introduced:

* In-memory lists will be replaced with database tables.
* `DbContext` will be used for database access.
* Relationships between Books, Authors, and Categories will be configured.
* LINQ queries will execute against the database.
* Repository or service-layer database operations can be introduced.
* Search and pagination can be translated into SQL queries.
* Database constraints can enforce unique ISBNs and category names.

---

# Testing

The API can be tested using Swagger UI or Postman.

Swagger is available at:

```text
https://localhost:7022/swagger
```

### Recommended Testing Order

1. Create and test Authors.
2. Create and test Categories.
3. Create Books using existing Authors and Categories.
4. Test Book CRUD operations.
5. Test Search and Pagination.
6. Test the Reports endpoint.
7. Test invalid requests and verify the returned status codes.

---

# Evidence

Screenshots should demonstrate:

* Swagger API endpoints.
* Successful Author creation.
* Author validation / conflict response.
* Category creation and validation.
* Successful Book creation.
* Invalid Book request.
* Book search and pagination.
* Book deletion.
* Reports response.

---

# Conclusion

This project demonstrates the implementation of a basic RESTful BookStore API using ASP.NET Core Web API, DTOs, service-layer business logic, AutoMapper, in-memory data, validation, HTTP status codes, searching, pagination, and management reporting.
