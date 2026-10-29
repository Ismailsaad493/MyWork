using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IReturnDetailService
    {
        Task<IEnumerable<ReturnDetailMaster>> GetAllDetailsAsync();
        Task<ReturnDetailMaster> CreateDetailAsync(ReturnDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(string id, ReturnDetailUpdateDto dto);
    }

    public class ReturnDetailService : IReturnDetailService
    {
        private readonly IReturnDetailRepository _repo;

        public ReturnDetailService(IReturnDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ReturnDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ReturnDetailMaster> CreateDetailAsync(ReturnDetailCreateDto dto)
        {
            var detail = new ReturnDetailMaster
            {
                RId = dto.RId,
                IssueDetailId = dto.IssueDetailId,
                Condition = dto.Condition
            };

            return await _repo.AddAsync(detail);
        }

        public async Task<bool> UpdateDetailAsync(string id, ReturnDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.RId = dto.RId;
            existing.IssueDetailId = dto.IssueDetailId;
            existing.Condition = dto.Condition;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}