using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(string customerId);
        Task<IEnumerable<CustomerDto>> GetCustomersByCityAsync(string city);
        Task AddCustomerAsync(CustomerDto customer);
        Task UpdateCustomerContactNameAsync(string customerId, string contactName);
        Task UpdateCustomerCityAsync(string customerId, string city);
    }
}
