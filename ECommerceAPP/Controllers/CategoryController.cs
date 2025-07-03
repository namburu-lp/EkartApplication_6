using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                _categoryRepository.AddCategory(categoryDto);
                return Ok("Record Added Successfully!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryRepository.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("edit/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] string description)
        {
            try
            {
                var category = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.CategoryID == id);
                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                _categoryRepository.UpdateCategory(id, description);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
    
