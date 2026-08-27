using ProductsCategoriesApi.DTOs;

namespace ProductsCategoriesApi.Services
{
    public interface ICategoryService
    {
        public List<CategoryResponse> GetAllCategories();
        public CategoryResponse AddCategory(CreateCategoryRequest reques);
        public CategoryResponse? GetCateogoryById(int id);
        public CategoryResponse? UpdateCategory(int id, UpdateCategoryRequest request);
        public bool DeleteCategory(int id);
    }
}
