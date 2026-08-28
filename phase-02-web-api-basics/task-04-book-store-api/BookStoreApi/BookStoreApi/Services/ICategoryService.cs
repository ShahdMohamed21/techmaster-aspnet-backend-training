using BookStoreApi.DTOs.Categories;

namespace BookStoreApi.Services
{
    public interface ICategoryService
    {
       public List<CategoryResponse> GetAllCategories();

       public CategoryResponse CreateCategory(CreateCategoryRequest request);

       public CategoryResponse? GetCategoryById(int id);

       public CategoryResponse? UpdateCategory(int id, UpdateCategoryRequest request);

       public bool DeleteCategory(int id);
    }
}
