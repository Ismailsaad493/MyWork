using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentMaster>> GetAllAsync();
        Task<StudentMaster?> GetByIdAsync(string id);
        Task<StudentMaster> AddAsync(StudentMaster student);
        Task UpdateAsync(StudentMaster student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly NewBooksDbContext _context;
        public StudentRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<StudentMaster>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Department) // Joins the Department data
                .ToListAsync();
        }

        public async Task<StudentMaster?> GetByIdAsync(string id) => await _context.Students.FindAsync(id);

        public async Task<StudentMaster> AddAsync(StudentMaster student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task UpdateAsync(StudentMaster student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}