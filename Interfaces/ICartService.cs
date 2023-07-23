using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartItems();
        Task<Cart> AddCartItem(AddCartItemRequest cart);
        Task DeleteCartItem(int id);
    }
}
