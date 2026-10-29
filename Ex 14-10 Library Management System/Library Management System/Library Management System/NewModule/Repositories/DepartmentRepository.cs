using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentMaster>> GetAllAsync();
        Task<DepartmentMaster?> GetByIdAsync(string id);
        Task<DepartmentMaster> AddAsync(DepartmentMaster department);
        Task UpdateAsync(DepartmentMaster department);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly NewBooksDbContext _context;
        public DepartmentRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<DepartmentMaster>> GetAllAsync() => await _context.Departments.ToListAsync();

        public async Task<DepartmentMaster?> GetByIdAsync(string id) => await _context.Departments.FindAsync(id);

        public async Task<DepartmentMaster> AddAsync(DepartmentMaster department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department; // EF Core will automatically fetch the new "D1" ID here
        }

        public async Task UpdateAsync(DepartmentMaster department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}