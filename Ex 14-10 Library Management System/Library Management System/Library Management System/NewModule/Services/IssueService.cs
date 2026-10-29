using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueMaster>> GetAllIssuesAsync();
        Task<IssueMaster> CreateIssueAsync(IssueCreateDto dto);
        Task<bool> UpdateIssueAsync(string id, IssueUpdateDto dto);
    }

    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _repo;

        public IssueService(IIssueRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IssueMaster>> GetAllIssuesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IssueMaster> CreateIssueAsync(IssueCreateDto dto)
        {
            var issue = new IssueMaster
            {
                SId = dto.SId,
                IssueDate = dto.IssueDate,
                Status = dto.Status
            };

            return await _repo.AddAsync(issue);
        }

        public async Task<bool> UpdateIssueAsync(string id, IssueUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SId = dto.SId;
            existing.IssueDate = dto.IssueDate;
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}