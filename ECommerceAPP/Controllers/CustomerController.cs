using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDto customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
            return Ok("Record Created Successfully");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersByCity(string city)
        {
            var customers = await _customerRepository.GetCustomersByCityAsync(city);
            return Ok(customers);
        }

        [HttpPut("Edit/{customerId}")]
        public async Task<IActionResult> UpdateCustomerContactName(string customerId, [FromBody] string contactName)
        {
            await _customerRepository.UpdateCustomerContactNameAsync(customerId, contactName);
            return NoContent();
        }

        [HttpPatch("Edit/{customerId}")]
        public async Task<IActionResult> UpdateCustomerCity(string customerId, [FromBody] string city)
        {
            await _customerRepository.UpdateCustomerCityAsync(customerId, city);
            return NoContent();
        }
    }
}

