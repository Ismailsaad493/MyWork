using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
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
                IssueDate = i.IssueDate,
                Status = i.Status,
                StatusName = i.Status == 1 ? "open" : (i.Status == 2 ? "close" : "unknown"),
                Details = i.Details?.Select(d => new IssueDetailResponseDto
                {
                    IssueDetailId = d.IssueDetailId,
                    IssueId = d.IssueId,
                    BookCopyId = d.BookCopyId,
                    DueDate = d.DueDate,
                    Status = d.Status,
                    StatusName = d.Status == 1 ? "issued" : (d.Status == 2 ? "returned" : "unknown")
                }).ToList() ?? new List<IssueDetailResponseDto>()
            }).ToList();
        }

        public async Task<IssueResponseDto> CreateIssueAsync(IssueCreateDto dto)
        {
            var issue = new IssueMaster
            {
                SId = dto.SId,
                IssueDate = DateOnly.FromDateTime(dto.IssueDate),

                // FIXED: Changed to IssueDetail (singular)
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
                IssueDate = savedIssue.IssueDate,
                Status = savedIssue.Status,
                StatusName = savedIssue.Status == 1 ? "open" : (savedIssue.Status == 2 ? "close" : "unknown"),
                Details = savedIssue.Details?.Select(d => new IssueDetailResponseDto
                {
                    IssueDetailId = d.IssueDetailId,
                    IssueId = d.IssueId,
                    BookCopyId = d.BookCopyId,
                    DueDate = d.DueDate,
                    Status = d.Status,
                    StatusName = d.Status == 1 ? "issued" : (d.Status == 2 ? "returned" : "unknown")
                }).ToList() ?? new List<IssueDetailResponseDto>()
            };
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

        public async Task<bool> DeleteIssueAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}