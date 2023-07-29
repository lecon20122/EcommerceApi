using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int id);
        bool IsCategoryNameExists(string name);
        bool IsCategoryExists(int id);

        bool IsCategoryNotExists(int id);
    }
}
