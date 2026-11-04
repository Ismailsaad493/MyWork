using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IIssueDetailService
    {
        Task<IEnumerable<IssueDetailResponseDto>> GetAllDetailsAsync(int pageNumber, int pageSize);
        Task<IssueDetailMaster> CreateDetailAsync(IssueDetailCreateDto dto);
        Task<IssueDetailMaster?> UpdateDetailAsync(int id, IssueDetailUpdateDto dto);
        Task<bool> DeleteDetailAsync(int id);
    }

    public class IssueDetailService : IIssueDetailService
    {
        private readonly IIssueDetailRepository _repo;
        public IssueDetailService(IIssueDetailRepository repo) => _repo = repo;

        public async Task<IEnumerable<IssueDetailResponseDto>> GetAllDetailsAsync(int pageNumber, int pageSize)
        {
            var details = await _repo.GetAllAsync(pageNumber, pageSize);

            return details.Select(d => new IssueDetailResponseDto
            {
                IssueDetailId = d.IssueDetailId,
                IssueId = d.IssueId,
                BookCopyId = d.BookCopyId,
                BookTitle = d.BookCopy?.Book?.Title ?? string.Empty,
                DueDate = d.DueDate,
                Status = d.Status
            }).ToList();
        }

        public async Task<IssueDetailMaster> CreateDetailAsync(IssueDetailCreateDto dto)
        {
            var detail = new IssueDetailMaster
            {
                IssueId = dto.IssueId,
                BookCopyId = dto.BookCopyId,
                DueDate = dto.DueDate, // Maps DateOnly directly
                Status = dto.Status
            };
            return await _repo.AddAsync(detail);
        }

        public async Task<IssueDetailMaster?> UpdateDetailAsync(int id, IssueDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.IssueId = dto.IssueId;
            existing.BookCopyId = dto.BookCopyId;
            existing.DueDate = dto.DueDate; // Maps DateOnly directly
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteDetailAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}