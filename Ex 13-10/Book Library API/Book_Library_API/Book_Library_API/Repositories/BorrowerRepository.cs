using Book_Library_API.Data;
using Book_Library_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_API.Repositories
{
    public interface IBorrowerRepository
    {
        Task<IEnumerable<Borrower>> GetAllAsync();
        Task<Borrower> AddAsync(Borrower borrower);
    }

    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _dbContext;

        public BorrowerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Borrower>> GetAllAsync() => await _dbContext.Borrowers.ToListAsync();

        public async Task<Borrower> AddAsync(Borrower borrower)
        {
            await _dbContext.Borrowers.AddAsync(borrower);
            await _dbContext.SaveChangesAsync();
            return borrower;
        }
    }
}