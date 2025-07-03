using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPP.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(Zecommerce123Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        //public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        //{
        //    var employee = _mapper.Map<Employee>(employeeDto);
        //    _context.Employees.Update(employee);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<EmployeeDto>(employee);
        //}
        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

    }
}
