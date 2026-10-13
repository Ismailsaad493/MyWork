using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}