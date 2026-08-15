using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class ProductQueryService
    {
        private List<Product> products = new List<Product>();

        public ProductQueryService()
        {
            products.Add(new Product
            {
                ProductId = 1,
                Name = "Laptop Pro 14",
                Category = "Electronics",
                Price = 45000,
                StockQuantity = 5,
                CreatedAt = new DateTime(2026, 1, 10),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 2,
                Name = "Wireless Mouse",
                Category = "Electronics",
                Price = 650,
                StockQuantity = 50,
                CreatedAt = new DateTime(2026, 2, 1),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 3,
                Name = "Office Chair",
                Category = "Furniture",
                Price = 3500,
                StockQuantity = 10,
                CreatedAt = new DateTime(2025, 12, 15),
                IsAvailable = true,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 4,
                Name = "Standing Desk",
                Category = "Furniture",
                Price = 8000,
                StockQuantity = 3,
                CreatedAt = new DateTime(2026, 3, 5),
                IsAvailable = true,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 5,
                Name = "Notebook Pack",
                Category = "Stationery",
                Price = 120,
                StockQuantity = 100,
                CreatedAt = new DateTime(2026, 1, 20),
                IsAvailable = true,
                SupplierName = "PaperSupplier"
            });

            products.Add(new Product
            {
                ProductId = 6,
                Name = "Pen Set",
                Category = "Stationery",
                Price = 75,
                StockQuantity = 200,
                CreatedAt = new DateTime(2026, 1, 25),
                IsAvailable = true,
                SupplierName = "PaperSupplier"
            });

            products.Add(new Product
            {
                ProductId = 7,
                Name = "Gaming Keyboard",
                Category = "Electronics",
                Price = 2500,
                StockQuantity = 7,
                CreatedAt = new DateTime(2026, 2, 12),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 8,
                Name = "Monitor 27 inch",
                Category = "Electronics",
                Price = 9000,
                StockQuantity = 4,
                CreatedAt = new DateTime(2026, 2, 20),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 9,
                Name = "Desk Lamp",
                Category = "Furniture",
                Price = 650,
                StockQuantity = 0,
                CreatedAt = new DateTime(2025, 11, 1),
                IsAvailable = false,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 10,
                Name = "Backpack",
                Category = "Accessories",
                Price = 1200,
                StockQuantity = 15,
                CreatedAt = new DateTime(2026, 3, 10),
                IsAvailable = true,
                SupplierName = "BagSupplier"
            });

            products.Add(new Product
            {
                ProductId = 11,
                Name = "USB-C Hub",
                Category = "Electronics",
                Price = 1250,
                StockQuantity = 12,
                CreatedAt = new DateTime(2026, 4, 1),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 12,
                Name = "Whiteboard Markers",
                Category = "Stationery",
                Price = 95,
                StockQuantity = 80,
                CreatedAt = new DateTime(2026, 2, 15),
                IsAvailable = true,
                SupplierName = "PaperSupplier"
            });

            products.Add(new Product
            {
                ProductId = 13,
                Name = "Ergonomic Mouse Pad",
                Category = "Accessories",
                Price = 350,
                StockQuantity = 25,
                CreatedAt = new DateTime(2026, 5, 1),
                IsAvailable = true,
                SupplierName = "BagSupplier"
            });

            products.Add(new Product
            {
                ProductId = 14,
                Name = "Meeting Table",
                Category = "Furniture",
                Price = 12500,
                StockQuantity = 2,
                CreatedAt = new DateTime(2025, 10, 20),
                IsAvailable = true,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 15,
                Name = "HD Webcam",
                Category = "Electronics",
                Price = 1800,
                StockQuantity = 6,
                CreatedAt = new DateTime(2026, 4, 17),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 16,
                Name = "Printer Paper Box",
                Category = "Stationery",
                Price = 450,
                StockQuantity = 30,
                CreatedAt = new DateTime(2026, 2, 28),
                IsAvailable = true,
                SupplierName = "PaperSupplier"
            });

            products.Add(new Product
            {
                ProductId = 17,
                Name = "Laptop Stand",
                Category = "Accessories",
                Price = 950,
                StockQuantity = 9,
                CreatedAt = new DateTime(2026, 3, 30),
                IsAvailable = true,
                SupplierName = "BagSupplier"
            });

            products.Add(new Product
            {
                ProductId = 18,
                Name = "Network Cable 5m",
                Category = "Electronics",
                Price = 150,
                StockQuantity = 60,
                CreatedAt = new DateTime(2026, 1, 5),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 19,
                Name = "Storage Cabinet",
                Category = "Furniture",
                Price = 6000,
                StockQuantity = 1,
                CreatedAt = new DateTime(2025, 9, 10),
                IsAvailable = true,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 20,
                Name = "Sticky Notes",
                Category = "Stationery",
                Price = 60,
                StockQuantity = 0,
                CreatedAt = new DateTime(2026, 5, 10),
                IsAvailable = false,
                SupplierName = "PaperSupplier"
            });

            products.Add(new Product
            {
                ProductId = 21,
                Name = "Noise Cancelling Headset",
                Category = "Electronics",
                Price = 5200,
                StockQuantity = 4,
                CreatedAt = new DateTime(2026, 3, 22),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 22,
                Name = "Desk Organizer",
                Category = "Accessories",
                Price = 300,
                StockQuantity = 40,
                CreatedAt = new DateTime(2026, 6, 1),
                IsAvailable = true,
                SupplierName = "BagSupplier"
            });

            products.Add(new Product
            {
                ProductId = 23,
                Name = "Projector",
                Category = "Electronics",
                Price = 22000,
                StockQuantity = 2,
                CreatedAt = new DateTime(2026, 4, 28),
                IsAvailable = true,
                SupplierName = "TechSupplier"
            });

            products.Add(new Product
            {
                ProductId = 24,
                Name = "Office Sofa",
                Category = "Furniture",
                Price = 15500,
                StockQuantity = 1,
                CreatedAt = new DateTime(2025, 8, 18),
                IsAvailable = true,
                SupplierName = "HomeSupplier"
            });

            products.Add(new Product
            {
                ProductId = 25,
                Name = "Calculator",
                Category = "Stationery",
                Price = 250,
                StockQuantity = 35,
                CreatedAt = new DateTime(2026, 1, 12),
                IsAvailable = true,
                SupplierName = "PaperSupplier"
            });
        }
        public void GetAvailableProducts()
        {
            var availableProducts = products
                .Where(p => p.IsAvailable)
                .ToList();

            Console.WriteLine("===== Available Products =====");

            foreach (var product in availableProducts)
            {
                Console.WriteLine(
                    $"{product.ProductId} - {product.Name} - {product.Price}"
                );
            }
        }

        public void FilterByCategory(string category)
        {
            var result = products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("===== Products By Category =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Category} - {product.Price}"
                );
            }
        }

        public void FilterByPriceRange(decimal min, decimal max)
        {
            if (min < 0 || max < 0)
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }

            if (max < min)
            {
                Console.WriteLine("Maximum price cannot be less than minimum price.");
                return;
            }

            var result = products
                .Where(p => p.Price >= min && p.Price <= max)
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("===== Products By Price Range =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Price}"
                );
            }
        }

        public void SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Keyword cannot be empty.");
                return;
            }

            var result = products
                .Where(p => p.Name.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("===== Search Results =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Category} - {product.Price}"
                );
            }
        }

        public void SortByPriceAscending()
        {
            var result = products
                .OrderBy(p => p.Price)
                .ToList();

            Console.WriteLine("===== Products By Price Ascending =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Price}"
                );
            }
        }
        public void SortByPriceDescending()
        {
            var result = products
                .OrderByDescending(p => p.Price)
                .ToList();

            Console.WriteLine("===== Products By Price Descending =====");

            foreach (var product in result)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }
        }
        public void GroupByCategory()
        {
            var groups = products.GroupBy(p => p.Category);

            Console.WriteLine("===== Products Grouped By Category =====");

            foreach (var group in groups)
            {
                Console.WriteLine($"\n--- {group.Key} ---");

                foreach (var product in group)
                {
                    Console.WriteLine($"{product.Name} - {product.Price}");
                }
            }
        }

        public void CountProductsByCategory()
        {
            var groups = products
                .GroupBy(p => p.Category);

            Console.WriteLine("===== Product Count By Category =====");

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }
        }

        public void CalculateTotalStockValue()
        {
            var totalStockValue = products
                .Sum(p => p.Price * p.StockQuantity);

            Console.WriteLine("===== Total Stock Value =====");
            Console.WriteLine($"Total Stock Value : {totalStockValue}");
        }

        public void StockValueByCategory()
        {
            var groups = products
                .GroupBy(p => p.Category);

            Console.WriteLine("===== Stock Value By Category =====");

            foreach (var group in groups)
            {
                var stockValue = group.Sum(
                    p => p.Price * p.StockQuantity);

                Console.WriteLine(
                    $"{group.Key} : {stockValue}"
                );
            }
        }
        public void GetTop5ExpensiveProducts()
        {
            var result = products
                .OrderByDescending(p => p.Price)
                .Take(5)
                .ToList();

            Console.WriteLine("===== Top 5 Most Expensive Products =====");

            foreach (var product in result)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }
        }

        public void GetLowStockProducts()
        {
            var result = products
                .Where(p => p.StockQuantity <= 5)
                .ToList();

            Console.WriteLine("===== Low Stock Products =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - Stock: {product.StockQuantity}");
            }
        }
        public void GetOutOfStockProducts()
        {
            var result = products
                .Where(p => p.StockQuantity == 0 || !p.IsAvailable)
                .ToList();

            Console.WriteLine("===== Out of Stock Products =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - Stock: {product.StockQuantity} - Available: {product.IsAvailable}");
            }
        }
        public void GetProductSummaries()
        {
            var result = products
                .Select(p => new ProductSummary
                {
                    Name = p.Name,
                    Price = p.Price,
                    StockStatus = p.StockQuantity > 0
                        ? "In Stock"
                        : "Out of Stock"
                })
                .ToList();

            Console.WriteLine("===== Product Summaries =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Price} - {product.StockStatus}");
            }
        }
        public void GetSupplierReport()
        {
            var result = products
                .GroupBy(p => p.SupplierName)
                .Select(g => new SupplierReport
                {
                    SupplierName = g.Key,
                    ProductCount = g.Count(),
                    StockValue = g.Sum(p => p.Price * p.StockQuantity),
                    AveragePrice = g.Average(p => p.Price)
                })
                .ToList();

            Console.WriteLine("===== Supplier Report =====");

            foreach (var report in result)
            {
                Console.WriteLine($"Supplier: {report.SupplierName}");
                Console.WriteLine($"Products: {report.ProductCount}");
                Console.WriteLine($"Stock Value: {report.StockValue}");
                Console.WriteLine($"Average Price: {report.AveragePrice}");
                Console.WriteLine("-----------------------------");
            }
        }
        public void GetRecentlyAddedProducts()
        {
            var date = DateTime.Today.AddDays(-60);

            var result = products
                .Where(p => p.CreatedAt >= date)
                .ToList();

            Console.WriteLine("===== Recently Added Products =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.CreatedAt}");
            }
        }
        public void GetCategoryStatistics()
        {
            var result = products
                .GroupBy(p => p.Category)
                .Select(g => new CategoryStats
                {
                    Category = g.Key,
                    Count = g.Count(),
                    AveragePrice = g.Average(p => p.Price),
                    MaxPrice = g.Max(p => p.Price),
                    MinPrice = g.Min(p => p.Price),
                    TotalStockValue = g.Sum(
                        p => p.Price * p.StockQuantity)
                })
                .ToList();

            Console.WriteLine("===== Category Statistics =====");

            foreach (var stat in result)
            {
                Console.WriteLine($"Category: {stat.Category}");
                Console.WriteLine($"Count: {stat.Count}");
                Console.WriteLine($"Average Price: {stat.AveragePrice}");
                Console.WriteLine($"Max Price: {stat.MaxPrice}");
                Console.WriteLine($"Min Price: {stat.MinPrice}");
                Console.WriteLine($"Total Stock Value: {stat.TotalStockValue}");
                Console.WriteLine("-----------------------------");
            }
        }
        public void GetProductsAboveAveragePrice()
        {
            var averagePrice = products.Average(p => p.Price);

            var result = products
                .Where(p => p.Price > averagePrice)
                .ToList();

            Console.WriteLine("===== Products Above Average Price =====");
            Console.WriteLine($"Average Price: {averagePrice}");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Price}");
            }
        }

        public void SearchAndFilter(
    string category,
    decimal minPrice,
    decimal maxPrice,
    bool isAvailable)
        {
            if (minPrice < 0 || maxPrice < 0)
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }

            if (maxPrice < minPrice)
            {
                Console.WriteLine(
                    "Maximum price cannot be less than minimum price.");
                return;
            }

            var result = products
                .Where(p =>
                    p.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase))
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Where(p => p.IsAvailable == isAvailable)
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("===== Search + Filter Results =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.Name} - {product.Category} - {product.Price} - Available: {product.IsAvailable}");
            }
        }
        public void GetProductsPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                Console.WriteLine("Page number must be greater than 0.");
                return;
            }

            if (pageSize <= 0)
            {
                Console.WriteLine("Page size must be greater than 0.");
                return;
            }

            var result = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Console.WriteLine(
                $"===== Page {pageNumber} =====");

            foreach (var product in result)
            {
                Console.WriteLine(
                    $"{product.ProductId} - {product.Name} - {product.Price}");
            }
        }

    }
}