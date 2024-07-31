using AutoMapper;
using LogisticsApis.Data;
using LogisticsApis.DTOs;
using LogisticsApis.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogisticsApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(LogisticsAPIDbContext context)
        {
            _productRepository = new ProductRepository(context); // Instantiate the repository
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            var createdProduct = await _productRepository.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            await _productRepository.UpdateProductAsync(id, productDto);

            return NoContent();
        }

        // GET: api/products/bywarehouse/{warehouseId}
        [HttpGet("bywarehouse/{warehouseId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByWarehouse(int warehouseId)
        {
            var products = await _productRepository.GetProductsByWarehouseAsync(warehouseId);
            return Ok(products);
        }


        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
