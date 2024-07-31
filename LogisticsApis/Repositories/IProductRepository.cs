using LogisticsApis.DTOs;

namespace LogisticsApis.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<IEnumerable<ProductDto>> GetProductsByWarehouseAsync(int warehouseId);
        Task UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}
