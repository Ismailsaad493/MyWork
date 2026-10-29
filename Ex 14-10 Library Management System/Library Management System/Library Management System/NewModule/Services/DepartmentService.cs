using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentMaster>> GetAllDepartmentsAsync();
        Task<DepartmentMaster> CreateDepartmentAsync(DepartmentUpdateDto dto);
        Task<bool> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) => _repo = repo;

        public async Task<IEnumerable<DepartmentMaster>> GetAllDepartmentsAsync() => await _repo.GetAllAsync();

        public async Task<DepartmentMaster> CreateDepartmentAsync(DepartmentUpdateDto dto)
        {
            var dept = new DepartmentMaster { DName = dto.DName };
            return await _repo.AddAsync(dept);
        }

        public async Task<bool> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.DName = dto.DName;
            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}