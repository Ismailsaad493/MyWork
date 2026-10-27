using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Entities;
using Library_Management_System.NewModule.Repositories;

namespace Library_Management_System.NewModule.Services
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherResponseDto>> GetAllPublishersAsync();
        Task<PublisherResponseDto> CreatePublisherAsync(PublisherCreateDto dto);
    }

    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repo;
        public PublisherService(IPublisherRepository repo) => _repo = repo;

        public async Task<IEnumerable<PublisherResponseDto>> GetAllPublishersAsync()
        {
            var publishers = await _repo.GetAllAsync();
            return publishers.Select(p => new PublisherResponseDto(p.PId, p.PName));
        }

        public async Task<PublisherResponseDto> CreatePublisherAsync(PublisherCreateDto dto)
        {
            var publisher = new PublisherMaster { PName = dto.PName };
            var saved = await _repo.AddAsync(publisher);
            return new PublisherResponseDto(saved.PId, saved.PName);
        }
    }
}