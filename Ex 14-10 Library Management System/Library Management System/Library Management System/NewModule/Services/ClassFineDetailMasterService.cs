using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IFineDetailMasterService
    {
        Task<IEnumerable<FineDetailMaster>> GetAllDetailsAsync();
        Task<FineDetailMaster> CreateDetailAsync(FineDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(string id, FineDetailUpdateDto dto);
    }

    public class FineDetailMasterService : IFineDetailMasterService
    {
        private readonly IFineDetailMasterRepository _repo;

        public FineDetailMasterService(IFineDetailMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FineDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<FineDetailMaster> CreateDetailAsync(FineDetailCreateDto dto)
        {
            var detail = new FineDetailMaster
            {
                FineId = dto.FineId,
                ChargeType = dto.ChargeType,
                Amount = dto.Amount
            };
            return await _repo.AddAsync(detail);
        }

        public async Task<bool> UpdateDetailAsync(string id, FineDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.FineId = dto.FineId;
            existing.ChargeType = dto.ChargeType;
            existing.Amount = dto.Amount;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}