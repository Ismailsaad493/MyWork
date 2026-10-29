using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentMaster>> GetAllStudentsAsync();
        Task<StudentMaster> CreateStudentAsync(StudentUpdateDto dto);
        Task<bool> UpdateStudentAsync(string id, StudentUpdateDto dto);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task<IEnumerable<StudentMaster>> GetAllStudentsAsync() => await _repo.GetAllAsync();

        public async Task<StudentMaster> CreateStudentAsync(StudentUpdateDto dto)
        {
            var student = new StudentMaster
            {
                SName = dto.SName,
                DId = dto.DId
            };
            return await _repo.AddAsync(student);
        }

        public async Task<bool> UpdateStudentAsync(string id, StudentUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SName = dto.SName;
            existing.DId = dto.DId;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}