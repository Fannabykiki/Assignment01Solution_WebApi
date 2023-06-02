using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObject.ProductService;
using Common.DTO.Product;

namespace eStoreAPI.Controllers
{
    [Route("api/product-management")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await _productService.GetAllProductAsync();

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpGet("products/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var result = await _productService.UpdateAsync(id, updateProductRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpPost("products")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] AddProductRequest addProductRequest)
        {
            var result = await _productService.CreateAsync(addProductRequest);

            if (result == null) return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
