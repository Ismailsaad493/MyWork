using System.Threading.Tasks;
using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> AddAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByUsernameAsync(string username)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User> AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}