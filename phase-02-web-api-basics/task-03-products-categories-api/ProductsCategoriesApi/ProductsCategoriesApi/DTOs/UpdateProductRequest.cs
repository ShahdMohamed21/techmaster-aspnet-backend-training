using ProductsCategoriesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.DTOs
{
    public class UpdateProductRequest
    {
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
