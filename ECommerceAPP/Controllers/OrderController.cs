
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<OrderDto> _validator;

        public OrderController(IOrderRepository orderRepository, IValidator<OrderDto> validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(OrderDto order)
        {

            var ValidationResult = await _validator.ValidateAsync(order);
            if (!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            await _orderRepository.AddOrderAsync(order);
            return Ok("Record Added Successfully");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("edit/{customerId}")]
        public async Task<ActionResult> UpdateCustomerDetails(string customerId, OrderDto order)
        {
            var ValidationResult = await _validator.ValidateAsync(order);
            if (!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }
            await _orderRepository.UpdateCustomerDetailsAsync(customerId, order);
            return NoContent();
        }

        [HttpPatch("edit/{customerId}")]
        public async Task<ActionResult> PatchCustomerDetails(string customerId, OrderDto order)
        {

            await _orderRepository.PatchCustomerDetailsAsync(customerId, order);
            return NoContent();
        }


        [HttpGet("CustomerName/{customerName}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerName(string customerName)
        {
            var orders = await _orderRepository.GetOrdersByCustomerNameAsync(customerName);
            return Ok(orders);
        }

        [HttpGet("OrderDate/{orderDate}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByOrderDate(DateTime orderDate)
        {
            var orders = await _orderRepository.GetOrdersByOrderDateAsync(orderDate);
            return Ok(orders);
        }

        [HttpGet("BetweenDate/{fromDate}/{toDate}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersBetweenDates(DateTime fromDate, DateTime toDate)
        {
            var orders = await _orderRepository.GetOrdersBetweenDatesAsync(fromDate, toDate);
            return Ok(orders);
        }

        [HttpGet("HighestOrderCustomer")]
        public async Task<ActionResult<string>> GetHighestOrderCustomer()
        {
            var customerId = await _orderRepository.GetHighestOrderCustomerAsync();
            return Ok(customerId);
        }
    }
}
