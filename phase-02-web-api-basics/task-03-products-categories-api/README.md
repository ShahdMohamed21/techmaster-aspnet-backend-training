# Feature 04 - Stock Reports

## Overview

This feature provides useful stock and inventory reports for store management.

The reports calculate total stock value, stock value per category, low-stock products, out-of-stock products, and product counts by category.

---

## Endpoint

### Get Stock Reports

**GET**

```text
/api/Products/Reports
```

### Description

Returns a summary of the current product stock and inventory statistics.

### Response

**200 OK**

Example:

```json
{
  "totalStockValue": 235000,
  "stockValuePerCategory": {
    "1": 231000,
    "2": 53000,
    "3": 12100,
    "4": 12300
  },
  "lowStockProducts": [
    {
      "productId": 2,
      "name": "Mouse",
      "price": 500,
      "stockQuantity": 3,
      "categoryId": 1,
      "isAvailable": true,
      "supplierName": "Tech Supplier",
      "createdAt": "2026-08-27T20:00:00"
    }
  ],
  "outOfStockProducts": [],
  "productCountsByCategory": {
    "1": 5,
    "2": 3,
    "3": 4,
    "4": 3
  }
}
```

> The exact values in the response depend on the current seed data and any products added or updated during testing.

---

## Business Rules

The report includes:

1. **Total Stock Value**

   * Calculates the total value of all products in stock.
   * Formula:

   ```text
   Price × Stock Quantity
   ```

2. **Stock Value Per Category**

   * Groups products by `CategoryId`.
   * Calculates the total stock value for each category.

3. **Low Stock Products**

   * Returns products where:

   ```text
   StockQuantity > 0 && StockQuantity <= 5
   ```

4. **Out of Stock Products**

   * Returns products where:

   ```text
   StockQuantity == 0
   ```

5. **Products Count By Category**

   * Groups products by category.
   * Returns the number of products in each category.

---

## Implementation

The report logic is implemented inside the `ProductService` rather than the controller.

```csharp
public Reports GetReports()
{
    Reports reports = new Reports();

    reports.TotalStockValue =
        products.Sum(x => x.Price * x.StockQuantity);

    reports.OutOfStockProducts =
        products
        .Where(x => x.StockQuantity == 0)
        .Select(x => new ProductResponse
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            StockQuantity = x.StockQuantity,
            CategoryId = x.CategoryId,
            IsAvailable = x.IsAvailable,
            SupplierName = x.SupplierName,
            CreatedAt = x.CreatedAt
        })
        .ToList();

    reports.LowStockProducts =
        products
        .Where(x => x.StockQuantity > 0 && x.StockQuantity <= 5)
        .Select(x => new ProductResponse
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Price = x.Price,
            StockQuantity = x.StockQuantity,
            CategoryId = x.CategoryId,
            IsAvailable = x.IsAvailable,
            SupplierName = x.SupplierName,
            CreatedAt = x.CreatedAt
        })
        .ToList();

    reports.StockValuePerCategory =
        products
        .GroupBy(x => x.CategoryId)
        .ToDictionary(
            x => x.Key.ToString(),
            x => x.Sum(p => p.Price * p.StockQuantity)
        );

    reports.ProductCountsByCategory =
        products
        .GroupBy(x => x.CategoryId)
        .ToDictionary(
            x => x.Key.ToString(),
            x => x.Count()
        );

    return reports;
}
```

---

## DTO

The `Reports` DTO contains all required report information:

```csharp
public class Reports
{
    public decimal TotalStockValue { get; set; }

    public Dictionary<string, decimal> StockValuePerCategory { get; set; }

    public List<ProductResponse> LowStockProducts { get; set; }

    public List<ProductResponse> OutOfStockProducts { get; set; }

    public Dictionary<string, int> ProductCountsByCategory { get; set; }
}
```

---

## Testing

### Swagger

The endpoint can be tested using Swagger:

```text
GET /api/Products/Reports
```

Expected result:

```text
200 OK
```

The response should contain all five required report sections.

### Postman

Request:

```text
GET https://localhost:7194/api/Products/Reports
```

Expected status:

```text
200 OK
```

---

## Evidence

The following evidence is required for this feature:

* [x] Swagger endpoint visible
* [x] Postman example
* [x] README route explanation
* [x] Screenshot of successful response

---

## Common Mistakes Avoided

* Report logic is implemented in the service layer, not inside the controller.
* Stock value is calculated using `Price × StockQuantity`.
* Low-stock and out-of-stock products are handled separately.
* Products are grouped by category for category-based statistics.
* The API returns `200 OK` when the report is successfully generated.
