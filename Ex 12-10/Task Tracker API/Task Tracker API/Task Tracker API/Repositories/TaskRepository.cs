using Microsoft.EntityFrameworkCore;
using Task_Tracker_API.Entities;
using Task_Tracker_API.Repositories;

namespace TaskTrackerAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _dbContext.Tasks.ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(Guid id) =>
            await _dbContext.Tasks.FindAsync(id);

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}