using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _category;
        private readonly IApplicationFile _file;

        public ProductService(ApplicationDbContext context, ICategoryService category, IApplicationFile file)
        {
            _context = context;
            _category = category;
            _file = file;
        }

        public async Task<Product> AddProduct(AddProductRequest product)
        {
            if (_category.IsCategoryNotExists(product.CategoryId)) throw new Exception("the category is not existed");

            var newProduct = await _context.Products.AddAsync(new Product
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Quantity = product.Quantity,
            });

            await _context.SaveChangesAsync();

            await _file.UploadImages(product.Images, newProduct.Entity.Id, null);

            return newProduct.Entity;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            _file.Delete(product.Id);

            var result = _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImage)
                .ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var updatedProduct = _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return updatedProduct.Entity;
        }
    }
}
