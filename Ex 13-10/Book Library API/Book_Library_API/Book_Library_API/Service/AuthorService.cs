using Book_Library_API.Entities;
using Book_Library_API.Repositories;

namespace Book_Library_API.Service
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AddAuthorDto dto);
        Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto dto);
        Task<bool> DeleteAuthorAsync(int id);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _repository.GetAllAsync();
            return authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name });
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDto { Id = author.Id, Name = author.Name };
        }

        public async Task<AuthorDto> CreateAuthorAsync(AddAuthorDto dto)
        {
            var author = new Author { Name = dto.Name };
            var savedAuthor = await _repository.AddAsync(author);
            return new AuthorDto { Id = savedAuthor.Id, Name = savedAuthor.Name };
        }

        public async Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto dto)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;

            author.Name = dto.Name;
            await _repository.UpdateAsync(author);

            return new AuthorDto { Id = author.Id, Name = author.Name };
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}