using Task_Tracker_API.Entities;

namespace TaskTrackerAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
        Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto dto);
        Task<bool> MarkTaskCompleteAsync(Guid id);
    }
}