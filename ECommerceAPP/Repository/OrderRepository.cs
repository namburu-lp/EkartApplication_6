using AutoMapper;
using ECommerceApp.Models;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IMapper _mapper;

        public OrderRepository(Zecommerce123Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            var entity = _mapper.Map<Order>(order);
            //_context.Orders.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task UpdateCustomerDetailsAsync(string customerId, OrderDto order)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);
            if (entity != null)
            {
                // Map only the properties that can be updated
                if (order.CustomerId != null) entity.CustomerId = order.CustomerId;
                if (order.EmployeeId.HasValue) entity.EmployeeId = order.EmployeeId;
                if (order.OrderDate.HasValue) entity.OrderDate = order.OrderDate;
                if (order.RequiredDate.HasValue) entity.RequiredDate = order.RequiredDate;
                if (order.ShippedDate.HasValue) entity.ShippedDate = order.ShippedDate;
                if (order.ShipVia.HasValue) entity.ShipVia = order.ShipVia;
                if (order.Freight.HasValue) entity.Freight = order.Freight;
                if (order.ShipName != null) entity.ShipName = order.ShipName;
                if (order.ShipAddress != null) entity.ShipAddress = order.ShipAddress;
                if (order.ShipCity != null) entity.ShipCity = order.ShipCity;
                if (order.ShipRegion != null) entity.ShipRegion = order.ShipRegion;
                if (order.ShipPostalCode != null) entity.ShipPostalCode = order.ShipPostalCode;
                if (order.ShipCountry != null) entity.ShipCountry = order.ShipCountry;
                await _context.SaveChangesAsync();
            }
        }


        public async Task PatchCustomerDetailsAsync(string customerId, OrderDto order)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId);
            if (entity != null)
            {
                // Map only the properties that can be updated
                if (order.CustomerId != null) entity.CustomerId = order.CustomerId;
                if (order.EmployeeId.HasValue) entity.EmployeeId = order.EmployeeId;
                if (order.OrderDate.HasValue) entity.OrderDate = order.OrderDate;
                if (order.RequiredDate.HasValue) entity.RequiredDate = order.RequiredDate;
                if (order.ShippedDate.HasValue) entity.ShippedDate = order.ShippedDate;
                if (order.ShipVia.HasValue) entity.ShipVia = order.ShipVia;
                if (order.Freight.HasValue) entity.Freight = order.Freight;
                if (order.ShipName != null) entity.ShipName = order.ShipName;
                if (order.ShipAddress != null) entity.ShipAddress = order.ShipAddress;
                if (order.ShipCity != null) entity.ShipCity = order.ShipCity;
                if (order.ShipRegion != null) entity.ShipRegion = order.ShipRegion;
                if (order.ShipPostalCode != null) entity.ShipPostalCode = order.ShipPostalCode;
                if (order.ShipCountry != null) entity.ShipCountry = order.ShipCountry;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerNameAsync(string customerName)
        {
            var orders = await _context.Orders
                .Where(o => o.Customer.ContactName == customerName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByOrderDateAsync(DateTime orderDate)
        {
            var orders = await _context.Orders
            .Where(o => o.OrderDate == orderDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersBetweenDatesAsync(DateTime fromDate, DateTime toDate)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<string> GetHighestOrderCustomerAsync()
        {
            var customer = await _context.Orders
                .GroupBy(o => o.CustomerId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();
            return customer;
        }
    }
}
