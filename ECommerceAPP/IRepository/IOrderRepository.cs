using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderDto order);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task UpdateCustomerDetailsAsync(string customerId, OrderDto order);
        Task PatchCustomerDetailsAsync(string customerId, OrderDto order);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerNameAsync(string customerName);
        Task<IEnumerable<OrderDto>> GetOrdersByOrderDateAsync(DateTime orderDate);
        Task<IEnumerable<OrderDto>> GetOrdersBetweenDatesAsync(DateTime fromDate, DateTime toDate);
        Task<string> GetHighestOrderCustomerAsync();
    }
}
