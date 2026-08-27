# Task 03 - Products & Categories API

## Overview

This project is a RESTful Web API built with ASP.NET Core.

The API manages products and categories and provides CRUD operations, search and filtering, and stock reports.

The project follows a simple layered structure where controllers handle HTTP requests and services contain the business logic.

---

## Technologies

* C#
* ASP.NET Core Web API
* RESTful API
* Swagger / OpenAPI
* LINQ
* In-Memory Collections

---

## Project Structure

```text
ProductsCategoriesApi
│
├── Controllers
│   ├── ProductsController.cs
│   └── CategoriesController.cs
│
├── Services
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── ICategoryService.cs
│   └── CategoryService.cs
│
├── Models
│   ├── Product.cs
│   └── Category.cs
│
└── DTOs
    ├── CreateProductRequest.cs
    ├── UpdateProductRequest.cs
    ├── ProductResponse.cs
    └── Reports.cs
```

---

# Features

## Feature 01 - Product & Category CRUD

This feature provides basic CRUD operations for products and categories.

### Product Endpoints

| Method | Route                | Description                |
| ------ | -------------------- | -------------------------- |
| GET    | `/api/Products`      | Get all products           |
| GET    | `/api/Products/{id}` | Get product by ID          |
| POST   | `/api/Products`      | Create a new product       |
| PUT    | `/api/Products/{id}` | Update an existing product |
| DELETE | `/api/Products/{id}` | Delete a product           |

### Category Endpoints

| Method | Route                  | Description                 |
| ------ | ---------------------- | --------------------------- |
| GET    | `/api/Categories`      | Get all categories          |
| GET    | `/api/Categories/{id}` | Get category by ID          |
| POST   | `/api/Categories`      | Create a new category       |
| PUT    | `/api/Categories/{id}` | Update an existing category |
| DELETE | `/api/Categories/{id}` | Delete a category           |

---

## Product Validation

The API validates product data before creation or update.

### Category Validation

A product cannot be created or updated with a category that does not exist.

Example error:

```text
Category Does Not Exist
```

### Price Validation

Product price must be greater than zero.

```text
Price > 0
```

### Stock Validation

Stock quantity cannot be negative.

```text
StockQuantity >= 0
```

### Name Validation

Product name cannot be empty or whitespace.

---

# Feature 02 - Product Search and Filters

The API provides a single search endpoint that supports multiple optional filters.

### Endpoint

```text
GET /api/Products/search
```

### Available Query Parameters

| Parameter     | Type    | Description                             |
| ------------- | ------- | --------------------------------------- |
| `name`        | string  | Search products by name                 |
| `categoryId`  | int     | Filter by category                      |
| `minPrice`    | decimal | Minimum product price                   |
| `maxPrice`    | decimal | Maximum product price                   |
| `isAvailable` | bool    | Filter by availability                  |
| `lowStock`    | bool    | Return products with stock quantity ≤ 5 |

### Examples

Search by name:

```text
GET /api/Products/search?name=Laptop
```

Filter by category:

```text
GET /api/Products/search?categoryId=1
```

Filter by minimum price:

```text
GET /api/Products/search?minPrice=1000
```

Filter by maximum price:

```text
GET /api/Products/search?maxPrice=10000
```

Filter by price range:

```text
GET /api/Products/search?minPrice=1000&maxPrice=10000
```

Filter by availability:

```text
GET /api/Products/search?isAvailable=true
```

Filter low-stock products:

```text
GET /api/Products/search?lowStock=true
```

Multiple filters can also be combined:

```text
GET /api/Products/search?categoryId=1&minPrice=1000&maxPrice=10000&isAvailable=true
```

### Search Logic

The filters are optional.

Only the filters provided by the client are applied.

The search logic is implemented in `ProductService` using LINQ and `IQueryable`.

---

# Feature 03 - Search and Filters

The search functionality allows store staff to quickly find products using:

* Search by product name
* Filter by category
* Filter by minimum price
* Filter by maximum price
* Filter by availability
* Filter by low stock

Low stock is defined as:

```text
StockQuantity <= 5
```

The endpoint returns:

```text
200 OK
```

with the matching products.

---

# Feature 04 - Stock Reports

The API provides business reports about the current product stock.

### Endpoint

```text
GET /api/Products/Reports
```

### Report Information

The endpoint returns:

1. Total stock value
2. Stock value per category
3. Low-stock products
4. Out-of-stock products
5. Product count by category

---

## Total Stock Value

The total stock value is calculated using:

```text
Price × StockQuantity
```

Example:

```text
Laptop:
25000 × 8 = 200000
```

---

## Stock Value Per Category

Products are grouped by `CategoryId`.

The total stock value is calculated for every category:

```text
Price × StockQuantity
```

---

## Low Stock Products

Products with:

```text
StockQuantity > 0 && StockQuantity <= 5
```

are returned as low-stock products.

---

## Out of Stock Products

Products with:

```text
StockQuantity == 0
```

are returned as out-of-stock products.

---

## Product Counts By Category

Products are grouped by category and the number of products in each category is returned.

Example:

```json
{
  "1": 5,
  "2": 3,
  "3": 4,
  "4": 3
}
```

---

# Seed Data

The project contains seed data for categories and products to support testing of search, filters, and reports.

## Categories

The seeded categories include:

* Electronics
* Furniture
* Stationery
* Accessories

## Products

### Electronics

* Laptop
* Mouse
* Keyboard
* Monitor
* USB-C Hub

### Furniture

* Office Chair
* Desk
* Desk Lamp

### Stationery

* Notebook
* Pen Set
* Marker
* Paper Pack

### Accessories

* Backpack
* Mouse Pad
* Laptop Sleeve

The seed data provides enough variation to test:

* Name search
* Category filtering
* Price filtering
* Availability filtering
* Low-stock filtering
* Stock reports

---

# API Examples

## Get All Products

```http
GET /api/Products
```

Expected response:

```text
200 OK
```

---

## Get Product By ID

```http
GET /api/Products/1
```

Expected response:

```text
200 OK
```

If the product does not exist:

```text
404 Not Found
```

---

## Create Product

```http
POST /api/Products
```

Example request body:

```json
{
  "name": "Webcam",
  "categoryId": 1,
  "price": 1500,
  "stockQuantity": 10,
  "isAvailable": true,
  "supplierName": "Tech Supplier",
  "createdAt": "2026-08-28T10:00:00"
}
```

Expected response:

```text
201 Created
```

---

## Update Product

```http
PUT /api/Products/1
```

Example request body:

```json
{
  "name": "Updated Laptop",
  "categoryId": 1,
  "price": 28000,
  "stockQuantity": 10,
  "isAvailable": true,
  "supplierName": "Tech Supplier",
  "createdAt": "2026-08-28T10:00:00"
}
```

Expected response:

```text
200 OK
```

---

## Delete Product

```http
DELETE /api/Products/1
```

Expected response:

```text
200 OK
```

If the product does not exist:

```text
404 Not Found
```

---

# Error Handling

The API handles common invalid requests.

Examples include:

### Product Not Found

```text
404 Not Found
```

### Category Not Found

```text
Category Does Not Exist
```

### Invalid Price

```text
Price must be greater than zero
```

### Invalid Stock Quantity

```text
Quantity Can Not Be Negative
```

### Invalid Product Name

```text
Product name cannot be empty
```

---

# Swagger

Swagger/OpenAPI is enabled for testing and documenting the API.

After running the project, open Swagger and test the available endpoints.

The main product routes are:

```text
GET    /api/Products
GET    /api/Products/{id}
POST   /api/Products
PUT    /api/Products/{id}
DELETE /api/Products/{id}
GET    /api/Products/search
GET    /api/Products/Reports
```

---

# Postman Testing

The API can also be tested using Postman.

Example base URL:

```text
https://localhost:7194
```

Example requests:

```text
GET https://localhost:7194/api/Products
```

```text
GET https://localhost:7194/api/Products/search?name=Laptop
```

```text
GET https://localhost:7194/api/Products/search?lowStock=true
```

```text
GET https://localhost:7194/api/Products/Reports
```

---

# Expected Evidence

The following evidence should be included for the task:

* [x] Swagger endpoints visible
* [x] Product CRUD tested
* [x] Category CRUD tested
* [x] Search and filters tested
* [x] Postman examples
* [x] Stock reports tested
* [x] Successful response screenshots
* [x] README route explanations

---

# Common Mistakes Avoided

The implementation avoids the following common mistakes:

* Returning `200 OK` when a requested resource does not exist.
* Creating products with invalid category IDs.
* Putting business logic inside controllers.
* Allowing negative prices.
* Allowing negative stock quantities.
* Using separate endpoints unnecessarily for every search filter.
* Mixing low-stock and out-of-stock products in the reports.

---

# Architecture

The project separates responsibilities between controllers and services.

### Controllers

Controllers are responsible for:

* Receiving HTTP requests
* Calling the appropriate service
* Returning HTTP responses

### Services

Services are responsible for:

* Business logic
* Validation
* Searching and filtering
* CRUD operations
* Generating stock reports

This keeps the controllers simple and makes the business logic easier to maintain and test.

---

# Status

**Task 03 - Products & Categories API: Completed**

Implemented features:

* Product CRUD
* Category CRUD
* Product validation
* Product search
* Product filters
* Low-stock filtering
* Stock reports
* Seed data
* Swagger documentation
* Postman testing
