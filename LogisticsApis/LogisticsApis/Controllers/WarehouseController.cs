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
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(LogisticsAPIDbContext context)
        {
            _warehouseRepository = new WarehouseRepository(context); // Instantiate the repository
        }

        // GET: api/warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetProducts()
        {
            var warehouses = await _warehouseRepository.GetWarehousesAsync();
            return Ok(warehouses);
        }

        // GET: api/warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDto>> GetProduct(int id)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // POST: api/warehouses
        [HttpPost]
        public async Task<ActionResult<WarehouseDto>> CreateProduct(WarehouseDto WarehouseDto)
        {
            var createdProduct = await _warehouseRepository.CreateWarehouseAsync(WarehouseDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/warehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, WarehouseDto WarehouseDto)
        {
            await _warehouseRepository.UpdateWarehouseAsync(id, WarehouseDto);

            return NoContent();
        }

        // DELETE: api/warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _warehouseRepository.DeleteWarehouseAsync(id);
            return NoContent();
        }
    }
}
