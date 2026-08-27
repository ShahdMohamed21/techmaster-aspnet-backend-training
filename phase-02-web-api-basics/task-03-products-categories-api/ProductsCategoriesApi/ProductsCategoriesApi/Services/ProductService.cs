using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private List<Product> products= new List<Product>();
        int NextId = 1;
        public ProductService( ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

       public ProductResponse AddProduct(CreateProductRequest request)
        {
            if(_categoryService.GetCateogoryById(request.CategoryId)==null)
            {
                throw new ArgumentException("Category Does Not Exist");
            }
            if(request.Price<=0)
            {
                throw new ArgumentException("Price must be greater than zero");

            }
            if(request.StockQuantity<0)
            {
                throw new ArgumentException("Quantity Can Not Be Negative");

            }
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Product name cannot be empty");

            }
            var pro = new Product
            {
                ProductId = NextId++,
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId,
                IsAvailable = request.IsAvailable,
                SupplierName = request.SupplierName,
                CreatedAt = request.CreatedAt
            };
            products.Add(pro);
            ProductResponse product = new ProductResponse
            {
                ProductId = pro.ProductId,
                Name = pro.Name,
                Price = pro.Price,
                StockQuantity = pro.StockQuantity,
                CategoryId = pro.CategoryId,
                IsAvailable = pro.IsAvailable,
                SupplierName = pro.SupplierName,
                CreatedAt = pro.CreatedAt
            };
            return product;

        }

        public bool DeleteProduct(int id)
        {
           var product=products.FirstOrDefault(x=> x.ProductId == id);
            if(product==null)
            {
                return false;
            }
            products.Remove(product);
            return true;
        }

       public List<ProductResponse> GetAllProducts()
        {
            var _products = products.Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                CategoryId = x.CategoryId,
                SupplierName = x.SupplierName,
                CreatedAt = x.CreatedAt,
                IsAvailable = x.IsAvailable,
            }).ToList();
            return _products;

        }

       public ProductResponse? GetProductById(int id)
        {
            var product=products.FirstOrDefault(x=> x.ProductId == id);
            if (product==null)
            {
                return null;
            }
            ProductResponse _product = new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                SupplierName = product.SupplierName,
                CreatedAt = product.CreatedAt,
                IsAvailable = product.IsAvailable,
                StockQuantity = product.StockQuantity,
            };
            return _product;
        }

       public ProductResponse? UpdateProduct(int id, UpdateProductRequest request)
        {
            var product = products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return null;
            }
            if (_categoryService.GetCateogoryById(request.CategoryId) == null)
            {
                throw new ArgumentException("Category Does Not Exist");
            }
            if (request.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero");

            }
            if (request.StockQuantity < 0)
            {
                throw new ArgumentException("Quantity Can Not Be Negative");

            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Product name cannot be empty");

            }
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.SupplierName = request.SupplierName;
            product.CreatedAt = request.CreatedAt;
            product.IsAvailable = request.IsAvailable;
            product.StockQuantity = request.StockQuantity;
            product.Name = request.Name;
            ProductResponse _product = new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                SupplierName = product.SupplierName,
                CreatedAt = product.CreatedAt,
                IsAvailable = product.IsAvailable,
                StockQuantity = product.StockQuantity,
            };
            return _product;




        }
    }
}
