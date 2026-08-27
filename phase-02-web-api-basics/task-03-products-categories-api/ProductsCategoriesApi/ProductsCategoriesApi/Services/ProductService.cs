using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private List<Product> products = new List<Product>
        {
    new Product
    {
            ProductId = 1,
            Name = "Laptop",
            Price = 25000,
            StockQuantity = 8,
            CategoryId = 1,
            IsAvailable = true,
            SupplierName = "Tech Supplier",
             CreatedAt = DateTime.Now
     },

    new Product
    {
        ProductId = 2,
        Name = "Mouse",
        Price = 500,
        StockQuantity = 3,
        CategoryId = 1,
        IsAvailable = true,
        SupplierName = "Tech Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 3,
        Name = "Keyboard",
        Price = 1200,
        StockQuantity = 15,
        CategoryId = 1,
        IsAvailable = true,
        SupplierName = "Tech Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 4,
        Name = "Monitor",
        Price = 7000,
        StockQuantity = 6,
        CategoryId = 1,
        IsAvailable = true,
        SupplierName = "Tech Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 5,
        Name = "USB-C Hub",
        Price = 900,
        StockQuantity = 2,
        CategoryId = 1,
        IsAvailable = true,
        SupplierName = "Tech Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 6,
        Name = "Office Chair",
        Price = 4500,
        StockQuantity = 4,
        CategoryId = 2,
        IsAvailable = true,
        SupplierName = "Office Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 7,
        Name = "Desk",
        Price = 6000,
        StockQuantity = 10,
        CategoryId = 2,
        IsAvailable = true,
        SupplierName = "Office Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 8,
        Name = "Desk Lamp",
        Price = 800,
        StockQuantity = 7,
        CategoryId = 2,
        IsAvailable = true,
        SupplierName = "Office Supplier",
        CreatedAt = DateTime.Now
    },

   
    new Product
    {
        ProductId = 9,
        Name = "Notebook",
        Price = 100,
        StockQuantity = 20,
        CategoryId = 3,
        IsAvailable = true,
        SupplierName = "Stationery Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 10,
        Name = "Pen Set",
        Price = 150,
        StockQuantity = 30,
        CategoryId = 3,
        IsAvailable = true,
        SupplierName = "Stationery Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 11,
        Name = "Marker",
        Price = 80,
        StockQuantity = 12,
        CategoryId = 3,
        IsAvailable = true,
        SupplierName = "Stationery Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 12,
        Name = "Paper Pack",
        Price = 250,
        StockQuantity = 25,
        CategoryId = 3,
        IsAvailable = true,
        SupplierName = "Stationery Supplier",
        CreatedAt = DateTime.Now
    },

    
    new Product
    {
        ProductId = 13,
        Name = "Backpack",
        Price = 1200,
        StockQuantity = 9,
        CategoryId = 4,
        IsAvailable = true,
        SupplierName = "Accessories Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 14,
        Name = "Mouse Pad",
        Price = 300,
        StockQuantity = 2,
        CategoryId = 4,
        IsAvailable = true,
        SupplierName = "Accessories Supplier",
        CreatedAt = DateTime.Now
    },

    new Product
    {
        ProductId = 15,
        Name = "Laptop Sleeve",
        Price = 700,
        StockQuantity = 0,
        CategoryId = 4,
        IsAvailable = false,
        SupplierName = "Accessories Supplier",
        CreatedAt = DateTime.Now
    }
        };
        int NextId = 16;
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

       public List<ProductResponse> SearchProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? lowStock)
        {
            var _products = products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                _products = _products.Where(x => x.Name.Contains(name));
            }
            if (categoryId != null)
            {
                _products=_products.Where(x => x.CategoryId == categoryId);
            }
            if(minPrice != null)
            {
                _products = _products.Where(x=> x.Price>=minPrice);
            }
            if(maxPrice != null)
            {
                _products = _products.Where(x => x.Price <= maxPrice);
            }
            if(isAvailable != null)
            {
                _products = _products.Where(x => x.IsAvailable == isAvailable.Value);
            }
            if(lowStock == true)
            {
                _products = _products.Where(x => x.StockQuantity <= 5);
            }

            var pro= _products.Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable,
                SupplierName = x.SupplierName,
                CreatedAt = x.CreatedAt
            }).ToList();
            return pro;

        }
        public Reports GetReports()
        {
            Reports reports = new Reports();
            reports.TotalStockValue =products.Sum(x => x.Price * x.StockQuantity);
            reports.OutOfStockProducts = products.Where(x => x.StockQuantity == 0).Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable,
                SupplierName = x.SupplierName,
                CreatedAt = x.CreatedAt

            }).ToList();
           reports.LowStockProducts = products.Where(x => x.StockQuantity > 0 &&  x.StockQuantity <= 5).Select(x => new ProductResponse
           {
               ProductId = x.ProductId,
               Name = x.Name,
               Price = x.Price,
               StockQuantity = x.StockQuantity,
               CategoryId = x.CategoryId,
               IsAvailable = x.IsAvailable,
               SupplierName = x.SupplierName,
               CreatedAt = x.CreatedAt

           }).ToList();

            reports.StockValuePerCategory = products.GroupBy(x => x.CategoryId)
                .ToDictionary(
                    x => x.Key.ToString(),
                    x => x.Sum(p => p.Price * p.StockQuantity)
                );
            reports.ProductCountsByCategory = products.GroupBy(x => x.CategoryId).ToDictionary(
             x => x.Key.ToString(),
                x => x.Count()
    );
            return reports;


        }
    }
}
