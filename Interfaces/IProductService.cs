using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> AddProduct(AddProductRequest product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
