using AutoMapper;
using BookStoreApi.DTOs.Categories;
using BookStoreApi.Models;

namespace BookStoreApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly List<Category> categories = new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                Name = "Programming",
                Description = "Programming and software development books",
                IsActive = true
            },

            new Category
            {
                CategoryId = 2,
                Name = "Science",
                Description = "Science and technology books",
                IsActive = true
            },

            new Category
            {
                CategoryId = 3,
                Name = "History",
                Description = "History books",
                IsActive = false
            }
        };

        private int _nextId = 4;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<CategoryResponse> GetAllCategories()
        {
            var Categories = _mapper.Map<List<CategoryResponse>>(categories);

            return Categories;
        }

        public CategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Category Name Is Required");
            }

            bool exists = categories.Any(c =>c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                throw new ArgumentException("Category Name Already Exists");
            }

            var category = _mapper.Map<Category>(request);

            category.CategoryId = _nextId++;

            categories.Add(category);

            var ResponeCategory = _mapper.Map<CategoryResponse>(category);

            return ResponeCategory;
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }
            var ResponeCategory= _mapper.Map<CategoryResponse>(category);
            return ResponeCategory;
        }

        public CategoryResponse? UpdateCategory( int id, UpdateCategoryRequest request)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Category Name Is Required");
            }

            bool duplicateName = categories.Any(c =>c.CategoryId != id &&
                c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            if (duplicateName)
            {
                throw new ArgumentException("Category Name Already Exists");
            }
            _mapper.Map(request, category);

            var ResponeCategory= _mapper.Map<CategoryResponse>(category);
            return ResponeCategory;
        }

        public bool DeleteCategory(int id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return false;
            }

            categories.Remove(category);

            return true;
        }
    }
}