using ProductCatalog.Services;

namespace ProductCatalog.UI
{
    public class ConsoleMenu
    {
        private ProductQueryService productService = new ProductQueryService();

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("==========================================");
                Console.WriteLine("       Product Catalog LINQ System");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. View Available Products");
                Console.WriteLine("2. Filter by Category");
                Console.WriteLine("3. Filter by Price Range");
                Console.WriteLine("4. Search by Name");
                Console.WriteLine("5. Sort by Price");
                Console.WriteLine("6. Group by Category");
                Console.WriteLine("7. Stock Value Reports");
                Console.WriteLine("8. Low Stock Products");
                Console.WriteLine("9. Supplier Report");
                Console.WriteLine("10. Pagination Demo");
                Console.WriteLine("11. Exit");
                Console.WriteLine("==========================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            productService.GetAvailableProducts();
                            break;

                        case "2":
                            FilterByCategory();
                            break;

                        case "3":
                            FilterByPriceRange();
                            break;

                        case "4":
                            SearchByName();
                            break;

                        case "5":
                            SortByPrice();
                            break;

                        case "6":
                            productService.GroupByCategory();
                            break;

                        case "7":
                            StockValueReports();
                            break;

                        case "8":
                            productService.GetLowStockProducts();
                            break;

                        case "9":
                            productService.GetSupplierReport();
                            break;

                        case "10":
                            Pagination();
                            break;

                        case "11":
                            isRunning = false;
                            Console.WriteLine("Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (isRunning)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void FilterByCategory()
        {
            Console.WriteLine();
            Console.WriteLine("========== Filter by Category ==========");
            Console.Write("Enter Category: ");

            string category = Console.ReadLine();

            productService.FilterByCategory(category);
        }

        private void FilterByPriceRange()
        {
            Console.WriteLine();
            Console.WriteLine("========== Filter by Price Range ==========");

            Console.Write("Enter Minimum Price: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Invalid minimum price.");
                return;
            }

            Console.Write("Enter Maximum Price: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Invalid maximum price.");
                return;
            }

            productService.FilterByPriceRange(minPrice, maxPrice);
        }

        private void SearchByName()
        {
            Console.WriteLine();
            Console.WriteLine("========== Search Product ==========");
            Console.Write("Enter Product Name: ");

            string keyword = Console.ReadLine();

            productService.SearchByName(keyword);
        }

        private void SortByPrice()
        {
            Console.WriteLine();
            Console.WriteLine("========== Sort by Price ==========");
            Console.WriteLine("1. Price Ascending");
            Console.WriteLine("2. Price Descending");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    productService.SortByPriceAscending();
                    break;

                case "2":
                    productService.SortByPriceDescending();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        private void StockValueReports()
        {
            Console.WriteLine();
            Console.WriteLine("========== Stock Value Reports ==========");
            Console.WriteLine("1. Total Stock Value");
            Console.WriteLine("2. Stock Value by Category");
            Console.WriteLine("3. Top 5 Most Expensive Products");
            Console.WriteLine("4. Out of Stock Products");

            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    productService.CalculateTotalStockValue();
                    break;

                case "2":
                    productService.StockValueByCategory();
                    break;

                case "3":
                    productService.GetTop5ExpensiveProducts();
                    break;

                case "4":
                    productService.GetOutOfStockProducts();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        private void Pagination()
        {
            Console.WriteLine();
            Console.WriteLine("========== Pagination ==========");

            Console.Write("Enter Page Number: ");

            if (!int.TryParse(Console.ReadLine(), out int pageNumber))
            {
                Console.WriteLine("Invalid page number.");
                return;
            }

            Console.Write("Enter Page Size: ");

            if (!int.TryParse(Console.ReadLine(), out int pageSize))
            {
                Console.WriteLine("Invalid page size.");
                return;
            }

            productService.GetProductsPage(pageNumber, pageSize);
        }
    }
}