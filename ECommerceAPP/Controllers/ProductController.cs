using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            try
            {
                var createdProduct = await _productRepository.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, new
                {
                    message = "Record Added Successfully!!",
                    data = createdProduct
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                return Ok(new
                {
                    message = "Displaying all products.",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.ProductId)
                return BadRequest("Product ID mismatch.");

            try
            {
                await _productRepository.UpdateProductAsync(productDto);
                return NoContent(); // 204
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("DiscontinuedProduct")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetDiscontinuedProducts()
        {
            try
            {
                var products = await _productRepository.SearchDiscontinuedAsync();
                return Ok(new
                {
                    message = "Displaying discontinued products.",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ByCategoryName/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string categoryName)
        {
            try
            {
                if (!_productRepository.CategoryNameExists(categoryName))
                    return NotFound("Category not found.");

                var products = await _productRepository.SearchByCategoryAsync(categoryName);
                return Ok(new
                {
                    message = $"Products in category: {categoryName}",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("ProductBySupplier/{companyName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsBySupplier(string companyName)
        {
            try
            {
                if (!_productRepository.CompanyNameExists(companyName))
                    return NotFound("Supplier not found.");

                var products = await _productRepository.SearchBySupplierAsync(companyName);
                return Ok(new
                {
                    message = $"Products supplied by: {companyName}",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("UnitInStock/{stock}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByStock(int stock)
        {
            try
            {
                if (!_productRepository.UnitInStockExists(stock))
                    return NotFound("No products with this stock level.");

                var products = await _productRepository.SearchByUnitStockAsync(stock);
                return Ok(new
                {
                    message = $"Products with UnitInStock = {stock}",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("UnitOnOrder")]
        public async Task<ActionResult> SearchByUnitsOnOrderGreaterThanZero()
        {
            try
            {
                var products = await _productRepository.SearchByUnitsOnOrderGreaterThanZeroAsync();
                return Ok(new
                {
                    message = "Displaying products with UnitsOnOrder > 0.",
                    data = products
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("OutOfStock")]
        public async Task<ActionResult<IEnumerable<object>>> GetOutOfStockProducts()
        {
            try
            {
                var products = await _productRepository.SearchByOutOfStockAsync();
                var result = products.Select(p => new
                {
                    p.ProductName,
                    p.UnitPrice
                });

                return Ok(new
                {
                    message = "Displaying out-of-stock products (ProductName and UnitPrice only).",
                    data = result
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                return Ok(new
                {
                    message = $"Product with ID {id} retrieved successfully.",
                    data = product
                });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productRepository.DeleteProductAsync(id);
                if (!result)
                    return NotFound(new { message = $"Product with ID {id} not found. Deletion failed." });

                return Ok(new { message = $"Product with ID {id} deleted successfully." });
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}

                

