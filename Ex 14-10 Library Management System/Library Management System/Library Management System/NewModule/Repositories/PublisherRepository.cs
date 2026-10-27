using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<PublisherMaster>> GetAllAsync();
        Task<PublisherMaster> AddAsync(PublisherMaster publisher);
    }

    public class PublisherRepository : IPublisherRepository
    {
        private readonly NewBooksDbContext _context;
        public PublisherRepository(NewBooksDbContext context) => _context = context;

        public async Task<IEnumerable<PublisherMaster>> GetAllAsync() => await _context.Publishers.ToListAsync();

        public async Task<PublisherMaster> AddAsync(PublisherMaster publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }
    }
}