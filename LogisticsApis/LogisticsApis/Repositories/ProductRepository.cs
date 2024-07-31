using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogisticsApis.Data;
using LogisticsApis.DTOs;
using LogisticsApis.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApis.Repositories
{
public class ProductRepository : IProductRepository
{
    private readonly LogisticsAPIDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(LogisticsAPIDbContext context)
    {
        _context = context;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDto>();
            cfg.CreateMap<ProductDto, Product>();
        }).CreateMapper();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
            var product = _mapper.Map<Product>(productDto);
            // Set the identity column to default value
            product.Id = default;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

    public async Task UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);
        product.Id = id;
            _mapper.Map(productDto, product);
        await _context.SaveChangesAsync();

    }
    public async Task<IEnumerable<ProductDto>> GetProductsByWarehouseAsync(int warehouseId)
    {
        var products = await _context.Products
            .Where(p => p.WarehouseId == warehouseId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

        public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
}