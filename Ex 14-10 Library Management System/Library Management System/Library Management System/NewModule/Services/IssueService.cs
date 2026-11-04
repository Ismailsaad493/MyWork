using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IIssueMasterService
    {
        Task<IEnumerable<IssueResponseDto>> GetAllIssuesAsync(int pageNumber, int pageSize);
        Task<IssueResponseDto> CreateIssueAsync(IssueCreateDto dto);
        Task<bool> UpdateIssueAsync(string id, IssueUpdateDto dto);
        Task<bool> DeleteIssueAsync(string id);
    }

    public class IssueService : IIssueMasterService
    {
        private readonly IIssueRepository _repo;

        public IssueService(IIssueRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IssueResponseDto>> GetAllIssuesAsync(int pageNumber, int pageSize)
        {
            var issues = await _repo.GetAllAsync();

            return issues.Select(i => new IssueResponseDto
            {
                IssueId = i.IssueId,
                SId = i.SId,
                IssueDate = i.IssueDate.ToDateTime(TimeOnly.MinValue),
                Status = i.Status,
                Details = i.Details?.Select(d => new IssueDetailResponseDto
                {
                    IssueDetailId = d.IssueDetailId,
                    IssueId = d.IssueId,
                    BookCopyId = d.BookCopyId,
                    DueDate = d.DueDate,
                    Status = d.Status
                }).ToList() ?? new List<IssueDetailResponseDto>()
            }).ToList();
        }

        public async Task<IssueResponseDto> CreateIssueAsync(IssueCreateDto dto)
        {
            var issue = new IssueMaster
            {
                SId = dto.SId,
                IssueDate = DateOnly.FromDateTime(dto.IssueDate),
                Status = dto.Status,
                Details = dto.Details.Select(d => new IssueDetailMaster
                {
                    BookCopyId = d.BookCopyId,
                    DueDate = d.DueDate,
                    Status = d.Status
                }).ToList()
            };

            var savedIssue = await _repo.AddAsync(issue);

            return new IssueResponseDto
            {
                IssueId = savedIssue.IssueId,
                SId = savedIssue.SId,
                IssueDate = savedIssue.IssueDate.ToDateTime(TimeOnly.MinValue),
                Status = savedIssue.Status,
                Details = savedIssue.Details?.Select(d => new IssueDetailResponseDto
                {
                    IssueDetailId = d.IssueDetailId,
                    IssueId = d.IssueId,
                    BookCopyId = d.BookCopyId,
                    DueDate = d.DueDate,
                    Status = d.Status
                }).ToList() ?? new List<IssueDetailResponseDto>()
            };
        }

        public async Task<bool> UpdateIssueAsync(string id, IssueUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SId = dto.SId;
            existing.IssueDate = DateOnly.FromDateTime(dto.IssueDate);
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteIssueAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}