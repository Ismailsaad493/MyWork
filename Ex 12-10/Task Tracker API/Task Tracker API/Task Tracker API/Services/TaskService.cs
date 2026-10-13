using Task_Tracker_API.Entities;
using Task_Tracker_API.Repositories;
using TaskTrackerAPI.Repositories;

namespace TaskTrackerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted
            });
        }

        public async Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto dto)
        {
            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            var savedTask = await _repository.AddAsync(newTask);

            return new TaskResponseDto
            {
                Id = savedTask.Id,
                Title = savedTask.Title,
                Description = savedTask.Description,
                IsCompleted = savedTask.IsCompleted
            };
        }

        public async Task<bool> MarkTaskCompleteAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return false;

            task.IsCompleted = true;
            await _repository.UpdateAsync(task);

            return true;
        }
    }
}