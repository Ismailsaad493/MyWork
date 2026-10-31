using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IFineMasterService
    {
        Task<IEnumerable<FineMaster>> GetAllFinesAsync();
        Task<FineMaster> CreateFineAsync(FineCreateDto dto);
        Task<bool> UpdateFineAsync(string id, FineUpdateDto dto);
    }

    public class FineMasterService : IFineMasterService
    {
        private readonly IFineMasterRepository _repo;

        public FineMasterService(IFineMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FineMaster>> GetAllFinesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<FineMaster> CreateFineAsync(FineCreateDto dto)
        {
            var fine = new FineMaster
            {
                IssueDetailId = dto.IssueDetailId,
                FineAmount = dto.FineAmount,
                Status = dto.Status,
                FineDate = DateTime.UtcNow
            };
            return await _repo.AddAsync(fine);
        }

        public async Task<bool> UpdateFineAsync(string id, FineUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IssueDetailId = dto.IssueDetailId;
            existing.FineAmount = dto.FineAmount;
            existing.Status = dto.Status;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}