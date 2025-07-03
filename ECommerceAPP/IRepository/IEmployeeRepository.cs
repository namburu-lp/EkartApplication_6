using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);
       // Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto);
        bool EmployeeExists(int id);
    }
}
