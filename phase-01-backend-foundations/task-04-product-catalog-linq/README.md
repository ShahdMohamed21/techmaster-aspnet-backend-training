# Task 04 - Product Catalog with LINQ

## Overview

A console-based Product Catalog application built with C# and LINQ.

The project simulates a small online-store catalog where users can search, filter, sort, group, and analyze products using LINQ queries.

The main goal of this task is to practice **LINQ, collections, filtering, sorting, grouping, projection, aggregation, and pagination** in a practical console application.

---

## Project Structure

```text
task-04-product-catalog/
│
├── README.md
│
└── ProductCatalog/
    │
    ├── Models/
    │   ├── Product.cs
    │   ├── ProductSummary.cs
    │   ├── CategoryStats.cs
    │   └── SupplierReport.cs
    │
    ├── Services/
    │   └── ProductQueryService.cs
    │
    ├── UI/
    │   └── ConsoleMenu.cs
    │
    └── Program.cs
```

---

## Product Model

Each product contains information such as:

* ProductId
* Name
* Category
* Price
* StockQuantity
* CreatedAt
* IsAvailable
* SupplierName
* Rating
* DiscountPercentage

The application starts with **25 seeded products** to make the LINQ queries meaningful.

---

## Features

The application supports the following operations:

1. View Available Products
2. Filter by Category
3. Filter by Price Range
4. Search by Name
5. Sort by Price
6. Group by Category
7. Stock Value Reports
8. Low Stock Products
9. Supplier Report
10. Pagination Demo
11. Exit

---

## LINQ Queries

The `ProductQueryService` contains the main LINQ operations:

* Get Available Products
* Filter Products by Category
* Filter Products by Price Range
* Search Products by Name
* Sort by Price Ascending
* Sort by Price Descending
* Group Products by Category
* Count Products per Category
* Calculate Total Stock Value
* Get Stock Value per Category
* Get Top 5 Most Expensive Products
* Get Low Stock Products
* Get Out of Stock Products
* Get Product Summaries
* Get Supplier Report
* Get Recently Added Products
* Get Category Statistics
* Get Products Above Average Price
* Search and Filter Products
* Get Products by Page using `Skip()` and `Take()`

---

## Validation

The application includes basic validation such as:

* Product price must be positive.
* Search is case-insensitive.
* Products can be filtered and searched using partial names.
* Pagination is handled using LINQ `Skip()` and `Take()`.

---

## Main LINQ Concepts Practiced

This task focuses on practical usage of:

* `Where()`
* `Select()`
* `OrderBy()`
* `OrderByDescending()`
* `GroupBy()`
* `Count()`
* `Sum()`
* `Average()`
* `FirstOrDefault()`
* `Take()`
* `Skip()`
* Anonymous objects
* Projection
* Aggregation
* Combining multiple LINQ operations

---

## Example

### Search by Product Name

The application allows the user to search for a product using part of its name without worrying about uppercase or lowercase letters.

For example:

```text
Search: laptop
```

can return products such as:

```text
Laptop Pro 14
Gaming Laptop
Laptop Stand
```

### Pagination

The application also demonstrates pagination using:

```csharp
Skip()
Take()
```

This simulates displaying products page by page instead of displaying the entire catalog at once.

---

## Reports

The application provides different reports using LINQ, including:

* Total stock value
* Stock value by category
* Number of products per category
* Low-stock products
* Out-of-stock products
* Most expensive products
* Products above average price
* Supplier reports
* Category statistics

---

## Technologies Used

* C#
* .NET
* LINQ
* Console Application
* Collections (`List<T>`)

No database is used in this task. Product data is stored in memory using a `List<Product>`.

---

## Learning Objectives

This task was designed to improve practical understanding of LINQ and how it can be used to process collections of objects.

By completing this task, I practiced:

* Searching and filtering collections
* Sorting data
* Grouping data
* Creating projections
* Performing calculations and reports
* Combining multiple LINQ operations
* Using `Skip()` and `Take()` for pagination
* Separating business logic from the console UI

---

## Status

**Completed**

The application implements the required Product Catalog features using C# and LINQ.
