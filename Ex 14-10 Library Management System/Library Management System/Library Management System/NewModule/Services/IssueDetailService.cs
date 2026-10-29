using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IIssueDetailService
    {
        Task<IEnumerable<IssueDetailMaster>> GetAllDetailsAsync();
        Task<IssueDetailMaster> CreateDetailAsync(IssueDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(int id, IssueDetailUpdateDto dto);
    }

    public class IssueDetailService : IIssueDetailService
    {
        private readonly IIssueDetailRepository _repo;

        public IssueDetailService(IIssueDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IssueDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IssueDetailMaster> CreateDetailAsync(IssueDetailCreateDto dto)
        {
            var detail = new IssueDetailMaster
            {
                IssueId = dto.IssueId,
                BookCopyId = dto.BookCopyId,
                DueDate = dto.DueDate,
                Status = dto.Status
            };

            return await _repo.AddAsync(detail);
        }

        public async Task<bool> UpdateDetailAsync(int id, IssueDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IssueId = dto.IssueId;
            existing.BookCopyId = dto.BookCopyId;
            existing.DueDate = dto.DueDate;
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}