using Book_Library_API.Data;
using Book_Library_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_API.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public AuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() => await _dbContext.Authors.ToListAsync();

        public async Task<Author?> GetByIdAsync(int id) => await _dbContext.Authors.FindAsync(id);

        public async Task<Author> AddAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null) return false;

            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}