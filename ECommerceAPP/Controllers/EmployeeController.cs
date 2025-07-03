using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<Employee> _validator;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IValidator<Employee> validator, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(new { message = "All employees retrieved successfully.", data = employees });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            return Ok(new { message = $"Employee with ID {id} retrieved successfully.", data = employee });
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var validationResult = await _validator.ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed.",
                    errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }

            var created = await _employeeRepository.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = created.EmployeeID }, new
            {
                message = "Employee added successfully.",
                data = created
            });
        }
    }
}

//        [HttpPut("{id}")]
//        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
//        {
//            if (id != employeeDto.EmployeeID)
//                return BadRequest(new { message = "Employee ID mismatch." });

//            if (!_employeeRepository.EmployeeExists(id))
//                return NotFound(new { message = $"Employee with ID {id} not found." });

//            var employee = _mapper.Map<Employee>(employeeDto);
//            var validationResult = await _validator.ValidateAsync(employee);

//            if (!validationResult.IsValid)
//            {
//                return BadRequest(new
//                {
//                    message = "Validation failed.",
//                    errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
//                });
//            }
//        }
//    }
//}
