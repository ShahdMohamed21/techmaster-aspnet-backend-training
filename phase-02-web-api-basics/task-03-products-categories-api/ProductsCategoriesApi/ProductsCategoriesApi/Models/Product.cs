using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.Models
{
    public class Product
    {   
     public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    public Category CategoryId { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public string SupplierName { get; set; }
    public DateTime CreatedAt { get; set; }

}
}
