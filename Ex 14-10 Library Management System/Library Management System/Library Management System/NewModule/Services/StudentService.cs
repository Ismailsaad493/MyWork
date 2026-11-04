using Library_Management_System.Entities;
using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using StudentDto = Library_Management_System.NewModule.Dtos.StudentDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IStudentService
    {
        // 1. Returns StudentDto to format the GET response cleanly
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();

        Task<StudentMaster> CreateStudentAsync(StudentUpdateDto dto);

        // 2. Returns StudentMaster? instead of bool for proper PUT response bodies
        Task<StudentMaster?> UpdateStudentAsync(string id, StudentUpdateDto dto);

        Task<bool> DeleteStudentAsync(string sId);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _repo.GetAllAsync();

            // Maps the entity to the DTO, dropping audit fields and nested loops
            return students.Select(s => new StudentDto
            {
                SId = s.SId,
                SName = s.SName,
                DId = s.DId,
                DepartmentName = s.Department?.DName
            }).ToList();
        }

        public async Task<StudentMaster> CreateStudentAsync(StudentUpdateDto dto)
        {
            var student = new StudentMaster
            {
                SName = dto.SName,
                DId = dto.DId
            };
            return await _repo.AddAsync(student);
        }

        public async Task<StudentMaster?> UpdateStudentAsync(string id, StudentUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null; // Return null instead of false

            existing.SName = dto.SName;
            existing.DId = dto.DId;

            await _repo.UpdateAsync(existing);
            return existing; // Returns the updated object
        }

        public async Task<bool> DeleteStudentAsync(string sId)
        {
            var student = await _repo.GetByIdAsync(sId);
            if (student == null) return false;

            await _repo.DeleteAsync(student);
            return true;
        }
    }
}