using LogisticsApis.DTOs;
using LogisticsApis.Models;
using AutoMapper;

namespace LogisticsApis.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Warehouse, WarehouseDto>();
        }
    }
}
