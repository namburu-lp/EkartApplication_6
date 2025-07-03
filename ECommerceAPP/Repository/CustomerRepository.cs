using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPP.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IMapper _mapper;

        public CustomerRepository(Zecommerce123Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersByCityAsync(string city)
        {
            var customers = await _context.Customers.Where(c => c.City == city).ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task AddCustomerAsync(CustomerDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerContactNameAsync(string customerId, string contactName)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.ContactName = contactName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCustomerCityAsync(string customerId, string city)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.City = city;
                await _context.SaveChangesAsync();
            }
        }
    }
}
  
