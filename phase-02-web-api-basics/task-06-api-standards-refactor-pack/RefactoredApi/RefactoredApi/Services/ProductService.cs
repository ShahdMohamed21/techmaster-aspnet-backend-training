using AutoMapper;
using RefactoredApi.DTOs;
using RefactoredApi.Models;

namespace RefactoredApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products = new List<Product>();
        private readonly IMapper mapper;
        public ProductService(IMapper _mapper)
        {
            mapper = _mapper;
            
        }
        private int nextId=1;

        public ProductResponse CreateProduct(CreateProductRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException("Name Is Required!");
            }
            if(request.Price<0)
            {
                throw new ArgumentException("Price Must Be Positive");
            }
            var product = mapper.Map <Product>(request);
            product.Id = nextId++;
            products.Add(product);
            var Productrespone=mapper.Map<ProductResponse>(product);
            return Productrespone;
            

        }

        public List<ProductResponse> GetAllProducts()
        {
            var productsresponse = mapper.Map<List<ProductResponse>>(products);
            return productsresponse;
        }

        public ProductResponse? GetProductById(int id)
        {
            var product = products.FirstOrDefault(p=> p.Id == id);
            if(product == null)
            {
                return null;
            }
            return mapper.Map<ProductResponse>(product);

        }
    }
}
