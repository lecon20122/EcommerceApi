using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            if (IsCategoryNameExists(category.Name)) throw new Exception($"This Name {category.Name} already exits");

            var newCategory = await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return newCategory.Entity;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public bool IsCategoryNameExists(string name)
        {
            return _context.Categories.Any(c => c.Name == name);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var updatedCategory = _context.Categories.Update(category);

            await _context.SaveChangesAsync();

            return updatedCategory.Entity;

        }

        public bool IsCategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool IsCategoryNotExists(int id)
        {
            return !IsCategoryExists(id);
        }
    }
}
