using Microsoft.AspNetCore.Http.HttpResults;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories=new List<Category>();
        int NextId = 1;
        public List<CategoryResponse> GetAllCategories()
        {
            var categories = _categories.Where(x => x.IsActive).Select(x => new CategoryResponse()
            {
                Categoryd = x.Categoryd,
                Name = x.Name,
                IsActive = x.IsActive,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            }).ToList();
            return categories;
            
        }
        public CategoryResponse AddCategory(CreateCategoryRequest request)
        {
            if(request.Name==null)
            {
                throw new ArgumentException("Category name is required");
            }
            var IsNameExist = _categories.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)); 
            if (IsNameExist)
            {
                throw new ArgumentException("Category name already exist");

            }
            var cat = new Category
            {
                Categoryd = NextId++,
                Name = request.Name,
                IsActive = request.IsActive,
                Description = request.Description,
                CreatedAt = request.CreatedAt,

            };
            _categories.Add(cat);
            CategoryResponse category = new CategoryResponse
            {
                Categoryd = cat.Categoryd,
                Name = cat.Name,
                Description = cat.Description,
                CreatedAt = cat.CreatedAt,
                IsActive = cat.IsActive,
            };
            return category;



        }
        public CategoryResponse? GetCateogoryById(int id)
        {
            var category = _categories.FirstOrDefault(x => x.Categoryd == id);
            if(category == null)
            {
                return null;
            }
            var cat = new CategoryResponse
            {
                Categoryd = category.Categoryd,
                Name = category.Name,
                IsActive = category.IsActive,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
            };
            return cat;
        }
        public CategoryResponse? UpdateCategory(int id, UpdateCategoryRequest request)
        {
            var category = _categories.FirstOrDefault(x => x.Categoryd == id);
            if (category == null)
            {
                return null;
            }
            var isNameExist = _categories.Any(x => x.Categoryd != id &&x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            if (isNameExist)
            {
                throw new ArgumentException("Category name already exists");
            }
            category.Name = request.Name;
            category.Description = request.Description;
            category.IsActive = request.IsActive;
            category.CreatedAt = request.CreatedAt;

            CategoryResponse categoryResponse = new CategoryResponse
            {
                Categoryd = category.Categoryd,
                Name = category.Name,
                IsActive = category.IsActive,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
            };
            return categoryResponse;
            
        }

        public bool DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(x => x.Categoryd == id);
            if (category == null)
            {
                return false;
            }
            _categories.Remove(category);
            return true;
        }
    }
}
