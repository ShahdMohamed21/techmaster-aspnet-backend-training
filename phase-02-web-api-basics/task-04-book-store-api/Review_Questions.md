## Review Questions

### Feature 01 - Authors API

* **Why did you use this route?**
  I used `/api/Authors` because it follows RESTful routing and represents the authors resource.

* **What DTOs did you create?**
  I created `CreateAuthorRequest`, `UpdateAuthorRequest`, and `AuthorResponse` DTOs.

* **What validation rules did you apply?**
  I validated that the author full name is required, the ID is unique, and authors with books cannot be deleted.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with an EF Core database and repository/DbContext operations.

### Feature 02 - Categories API

* **Why did you use this route?**
  I used `/api/Categories` because it follows RESTful routing and represents the categories resource.

* **What DTOs did you create?**
  I created `CreateCategoryRequest`, `UpdateCategoryRequest`, and `CategoryResponse` DTOs.

* **What validation rules did you apply?**
  I validated that the category name is required and unique, and inactive categories cannot be assigned to new books.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with an EF Core database and database queries through `DbContext`.

### Feature 03 - Books API

* **Why did you use this route?**
  I used `/api/Books` because it follows RESTful routing and represents the books resource.

* **What DTOs did you create?**
  I created `CreateBookRequest`, `UpdateBookRequest`, and `BookResponse` DTOs.

* **What validation rules did you apply?**
  I validated title, ISBN uniqueness, positive price, non-negative stock, and existing author and category IDs.

* **How will this change when EF Core is added?**
  The in-memory list will be replaced with EF Core entities, relationships, and database queries.

### Feature 04 - Search and Pagination

* **Why did you use this route?**
  I used `/api/Books` because searching and filtering are operations performed on the books resource.

* **What DTOs did you create?**
  I created `BookSearchRequest` to hold search, filter, and pagination parameters.

* **What validation rules did you apply?**
  I validated the search and filter parameters and used `pageNumber` and `pageSize` for pagination.

* **How will this change when EF Core is added?**
  Filtering, searching, and pagination will be performed directly in the database using LINQ and EF Core.

### Feature 05 - Reports Summary

* **Why did you use this route?**
  I used `/api/Reports` because it represents a management summary resource.

* **What DTOs did you create?**
  I created `ReportSummaryResponse` to return the required management statistics.

* **What validation rules did you apply?**
  No complex input validation is required because the report endpoint does not receive user input.

* **How will this change when EF Core is added?**
  The statistics will be calculated using database queries and LINQ instead of the in-memory books list.
