using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(int id);
        Task<Order> CreateOrderAsync();
    }
}
