using ProductsCategoriesApi.DTOs;

namespace ProductsCategoriesApi.Services
{
    public interface IProductService
    {
        public List<ProductResponse> GetAllProducts();
        public ProductResponse AddProduct(CreateProductRequest request);
        public ProductResponse? GetProductById(int id);
        public ProductResponse? UpdateProduct(int id, UpdateProductRequest request);
        public bool DeleteProduct(int id);
    }
}
