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
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly LogisticsAPIDbContext _context;
        private readonly IMapper _mapper;

        public WarehouseRepository(LogisticsAPIDbContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Warehouse, WarehouseDto>();
                cfg.CreateMap<WarehouseDto, Warehouse>();
            }).CreateMapper();
        }

        public async Task<IEnumerable<WarehouseDto>> GetWarehousesAsync()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            return _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        }

        public async Task<WarehouseDto> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public async Task<WarehouseDto> CreateWarehouseAsync(WarehouseDto warehouseDto)
        {
            var warehouse = _mapper.Map<Warehouse>(warehouseDto);
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public async Task UpdateWarehouseAsync(int id, WarehouseDto warehouseDto)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            warehouse.Id = id;
            _mapper.Map(warehouseDto, warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                // Handle not found error
                return;
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}
