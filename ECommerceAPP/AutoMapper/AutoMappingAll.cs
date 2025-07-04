using AutoMapper;
using ECommerceApp.Models;
using ECommerceAPP.DTOS;
using ECommerceAPP.Models;


namespace ECommerceAPP.AutoMapper
{
    public class AutoMappingAll : Profile
    {
        public AutoMappingAll()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Territory, TerritoryDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<ShipperDto, Shipper>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDemographic, CustomerDemographicDto>().ReverseMap();
            CreateMap<SupplierDto, Supplier>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
        }
    }
}



