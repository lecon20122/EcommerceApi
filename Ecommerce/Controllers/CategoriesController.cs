using Ecommerce.Authentication;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;


        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _category.GetCategories();

                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _category.GetCategory(id);

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = await _category.AddCategory(category);

                    return Ok(newCategory);
                }

                return BadRequest(new Response { Status = "Input Invalid", Message = "Invalid Model" });

            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedCategory = await _category.UpdateCategory(category);

                    return Ok(updatedCategory);
                }

                return BadRequest(new Response { Status = "Input Invalid", Message = ModelState.ErrorCount.ToString() });

            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _category.DeleteCategory(id);

                return Ok(new Response { Status = "Success", Message = "Category Deleted Successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
