using RefactoredApi.DTOs;

namespace RefactoredApi.Services
{
    public interface IProductService
    {
        public ProductResponse CreateProduct(CreateProductRequest request);
        public List<ProductResponse> GetAllProducts();
        public ProductResponse? GetProductById(int id);

    }
}
