using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetWithHistoryAsync(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student?> GetWithHistoryAsync(int id)
        {
            return await _context.Students
                .Include(s => s.BookLoans)
                .ThenInclude(bl => bl.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}