using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IReturnService
    {
        Task<IEnumerable<ReturnMaster>> GetAllReturnsAsync();
        Task<ReturnMaster> CreateReturnAsync(ReturnCreateDto dto);
        Task<bool> UpdateReturnAsync(string id, ReturnUpdateDto dto);
    }

    public class ReturnService : IReturnService
    {
        private readonly IReturnRepository _repo;

        public ReturnService(IReturnRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ReturnMaster>> GetAllReturnsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ReturnMaster> CreateReturnAsync(ReturnCreateDto dto)
        {
            var returnMaster = new ReturnMaster
            {
                SId = dto.SId,
                ReturnDate = System.DateTime.UtcNow
            };

            return await _repo.AddAsync(returnMaster);
        }

        public async Task<bool> UpdateReturnAsync(string id, ReturnUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SId = dto.SId;

            await _repo.UpdateAsync(existing);
            return true;
        }
    }
}