using Ecommerce.Data;
using Ecommerce.Enums;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Services
{

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _contextAccessor;
        private int _userId;

        public OrderService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICartService cartService, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;
            _contextAccessor = contextAccessor;
            _userId = int.Parse(_userManager.GetUserId(_contextAccessor.HttpContext.User));
        }

        public async Task<Order> CreateOrderAsync()
        {
            // get the user's cart
            List<Cart> userCart = await _cartService.GetCartItems();

            if (userCart.IsNullOrEmpty()) throw new Exception("user cart it empty, add item");

            decimal subTotal = 0;

            int shippingFees = 20; //TODO: get shipping fees from db

            Order newOrder = new Order
            {
                UserId = _userId,
                CreatedAt = DateTime.Now,
                Shipping = shippingFees,
                Status = OrderStatus.Pending,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in userCart)
            {
                subTotal += (decimal)item.Quantity * item.Product.Price;

                //var newOrderItem = new OrderProduct
                //{
                //    ProductId = item.ProductId,
                //    Quantity = item.Quantity,
                //    Price = item.Product.Price,
                //    Status = OrderStatus.Pending,
                //};

                newOrder.OrderProducts.Add(new OrderProduct
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Status = OrderStatus.Pending,
                });
            }

            newOrder.Total = subTotal + shippingFees;
            newOrder.SubTotal = subTotal;


            await _context.Orders.AddAsync(newOrder);

            await _cartService.ClearCart();

            await _context.SaveChangesAsync();

            return newOrder;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.UserId == _userId && o.Id == id);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                .Where(o => o.UserId == _userId)
                .ToListAsync();
        }
    }
}
