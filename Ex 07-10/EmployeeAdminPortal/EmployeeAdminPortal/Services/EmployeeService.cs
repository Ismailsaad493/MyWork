using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities; 
using EmployeeAdminPortal.Repositories;
using EmployeeAdminPortal.Repository;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary
            });
        }

        public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null) return null;

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
        }

        public async Task<EmployeeResponseDto> CreateEmployeeAsync(AddEmployeeDto dto)
        {
            var employeeEntity = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary
            };

            var savedEmployee = await _repository.AddAsync(employeeEntity);

            return new EmployeeResponseDto
            {
                Id = savedEmployee.Id,
                Name = savedEmployee.Name,
                Email = savedEmployee.Email,
                Phone = savedEmployee.Phone,
                Salary = savedEmployee.Salary
            };
        }

        public async Task<EmployeeResponseDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return null;

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            await _repository.UpdateAsync(employee);

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}