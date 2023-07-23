using Ecommerce.Authentication;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cart>>> Index()
        {
            try
            {
                return await _cartService.GetCartItems();
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpPost()]
        public async Task<ActionResult<Cart>> AddCartItem(AddCartItemRequest cart)
        {
            try
            {
                return await _cartService.AddCartItem(cart);
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await _cartService.DeleteCartItem(id);
                return Ok(new Response { Status = "Success", Message = "Cart Item Deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
