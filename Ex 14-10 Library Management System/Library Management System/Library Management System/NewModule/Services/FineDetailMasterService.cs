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
        Task<FineDetailMaster?> CreateDetailAsync(FineDetailCreateDto dto);
        Task<bool> UpdateDetailAsync(string id, FineDetailUpdateDto dto);
    }

    public class FineDetailMasterService : IFineDetailMasterService
    {
        private readonly IFineDetailMasterRepository _repo;
        private readonly IFineMasterRepository _fineRepo;

        public FineDetailMasterService(IFineDetailMasterRepository repo, IFineMasterRepository fineRepo)
        {
            _repo = repo;
            _fineRepo = fineRepo;
        }

        public async Task<IEnumerable<FineDetailMaster>> GetAllDetailsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<FineDetailMaster?> CreateDetailAsync(FineDetailCreateDto dto)
        {
            var fine = await _fineRepo.GetByIdAsync(dto.FineId);
            if (fine == null) return null;

            var detail = new FineDetailMaster
            {
                FineId = dto.FineId,
                ChargeType = dto.ChargeType,
                Amount = fine.FineAmount // Auto-populated
            };
            return await _repo.AddAsync(detail);
        }

        public async Task<bool> UpdateDetailAsync(string id, FineDetailUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            var fine = await _fineRepo.GetByIdAsync(dto.FineId);
            if (fine == null) return false;

            existing.FineId = dto.FineId;
            existing.ChargeType = dto.ChargeType;
            existing.Amount = fine.FineAmount; // Auto-synced

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}