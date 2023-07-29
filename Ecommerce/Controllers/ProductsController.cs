using Ecommerce.Authentication;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            try
            {
                var products = await _product.GetProducts();

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                var product = await _product.GetProduct(id);

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }

        }

        [HttpPost()]
        public async Task<IActionResult> AddProduct([FromForm] AddProductRequest product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newProduct = await _product.AddProduct(product);

                    return Ok(newProduct);
                }
                else
                {
                    return BadRequest(new Response { Status = "Error", Message = "Invalid Model" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.InnerException.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedProduct = await _product.UpdateProduct(product);

                    return Ok(updatedProduct);
                }
                else
                {
                    return BadRequest(new Response { Status = "Error", Message = "Invalid Model" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _product.DeleteProduct(id);

                return Ok(new Response { Status = "Success", Message = "Product Deleted Successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
