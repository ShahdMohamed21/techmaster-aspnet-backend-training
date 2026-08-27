namespace ProductsCategoriesApi.DTOs
{
    public class Reports
    {
        public decimal TotalStockValue { get; set; }

        public Dictionary<string, decimal> StockValuePerCategory { get; set; }

        public List<ProductResponse> LowStockProducts { get; set; }

        public List<ProductResponse> OutOfStockProducts { get; set; }

        public Dictionary<string, int> ProductCountsByCategory { get; set; }


    }
}
