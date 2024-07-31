using LogisticsApis.DTOs;

namespace LogisticsApis.Repositories
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<WarehouseDto>> GetWarehousesAsync();
        Task<WarehouseDto> GetWarehouseByIdAsync(int id);
        Task<WarehouseDto> CreateWarehouseAsync(WarehouseDto warehouseDto);
        Task UpdateWarehouseAsync(int id, WarehouseDto warehouseDto);
        Task DeleteWarehouseAsync(int id);
    }
}
