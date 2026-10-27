using Library_Management_System.Entities;
using Library_Management_System.Repositories;

namespace Library_Management_System.Service
{
    public interface IStudentService
    {
        Task<Student> RegisterStudentAsync(StudentDto dto);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<IEnumerable<BorrowHistoryDto>> GetStudentHistoryAsync(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Student> RegisterStudentAsync(StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                UniversityId = dto.UniversityId,
                Department = dto.Department,
                ContactNumber = dto.ContactNumber
            };

            return await _repository.AddAsync(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BorrowHistoryDto>> GetStudentHistoryAsync(int id)
        {
            var student = await _repository.GetWithHistoryAsync(id);
            if (student == null) return new List<BorrowHistoryDto>();

            return student.BookLoans.Select(bl => new BorrowHistoryDto
            {
                LoanId = bl.Id,
                BookTitle = bl.Book?.Title ?? "Unknown Title",
                BorrowDate = bl.LoanDate,
                ReturnDate = bl.ReturnDate,
                Status = bl.ReturnDate == null ? "Currently Borrowed" : "Returned"
            }).ToList();
        }
    }
}