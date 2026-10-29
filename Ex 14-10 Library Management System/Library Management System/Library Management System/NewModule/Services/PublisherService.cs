using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    // The Interface
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherMaster>> GetAllPublishersAsync();
        Task<PublisherMaster> CreatePublisherAsync(PublisherUpdateDto dto);
        Task<bool> UpdatePublisherAsync(int id, PublisherUpdateDto dto);
    }

    // The Implementation
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repo;

        public PublisherService(IPublisherRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PublisherMaster>> GetAllPublishersAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<PublisherMaster> CreatePublisherAsync(PublisherUpdateDto dto)
        {
            var publisher = new PublisherMaster
            {
                PName = dto.PName
            };
            return await _repo.AddAsync(publisher);
        }

        public async Task<bool> UpdatePublisherAsync(int id, PublisherUpdateDto dto)
        {
            var existingPublisher = await _repo.GetByIdAsync(id);
            if (existingPublisher == null) return false;

            existingPublisher.PName = dto.PName;

            await _repo.UpdateAsync(existingPublisher);
            return true;
        }
    }
}