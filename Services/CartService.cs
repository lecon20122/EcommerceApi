using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    [Authorize]
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private int _userId;


        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userId = int.Parse(_userManager.GetUserId(_contextAccessor.HttpContext.User));

        }

        public async Task<Cart> AddCartItem(AddCartItemRequest cart)
        {

            var isProductAlreadyInCart = _context.Carts.FirstOrDefault(c => c.ProductId == cart.ProductId && c.UserId == _userId);

            if (isProductAlreadyInCart != null)
            {
                if (isProductAlreadyInCart.Quantity < cart.Quantity)
                    throw new Exception("Product quantity is not enough");

                isProductAlreadyInCart.Quantity += cart.Quantity;
                await _context.SaveChangesAsync();
                return isProductAlreadyInCart;
            }
            else
            {

                var product = await _context.Products.FindAsync(cart.ProductId);

                if (product == null) throw new Exception("Product not found");

                if (product.Quantity < cart.Quantity) throw new Exception("Product quantity is not enough");

                var newCartItem = await _context.Carts.AddAsync(new Cart
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UserId = _userId
                }); ;

                await _context.SaveChangesAsync();

                return newCartItem.Entity;
            }

        }

        public async Task DeleteCartItem(int id)
        {
            var isCartExists = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == _userId);

            if (isCartExists == null) throw new Exception("Cart item not found");

            _context.Carts.Remove(isCartExists);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItems()
        {
            return await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == _userId)
                .ToListAsync();
        }
    }
}
