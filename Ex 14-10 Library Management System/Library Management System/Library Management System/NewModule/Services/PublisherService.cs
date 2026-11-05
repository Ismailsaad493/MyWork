using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherMaster>> GetAllPublishersAsync(int pageNumber);
        Task<PublisherMaster> CreatePublisherAsync(PublisherCreateDto dto);
        Task<bool> UpdatePublisherAsync(int id, PublisherUpdateDto dto);
        Task<bool> DeletePublisherAsync(int id);
        Task<bool> HardDeletePublisherAsync(int id);
    }

    // The Implementation
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repo;

        public PublisherService(IPublisherRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PublisherMaster>> GetAllPublishersAsync(int pageNumber)
        {
            int pageSize = 10;
            // FIX: Changed GetByIdAsync to GetAllAsync
            return await _repo.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<PublisherMaster> CreatePublisherAsync(PublisherCreateDto dto)
        {
            var publisher = new PublisherMaster
            {
                PName = dto.PName
            };
            return await _repo.AddAsync(publisher);
        }

        public async Task<bool> UpdatePublisherAsync(int id, PublisherUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.PName = dto.PName;
            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }

        public async Task<bool> HardDeletePublisherAsync(int id)
        {
            return await _repo.HardDeleteAsync(id);
        }
    }
}