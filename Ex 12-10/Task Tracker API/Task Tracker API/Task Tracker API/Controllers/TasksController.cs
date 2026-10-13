using Microsoft.AspNetCore.Mvc;
using Task_Tracker_API.Entities;
using TaskTrackerAPI.Services;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskRequestDto dto)
        {
            var createdTask = await _taskService.CreateTaskAsync(dto);

            return Created($"/api/tasks/{createdTask.Id}", createdTask);
        }
        [HttpPatch("{id:guid}/complete")]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            var success = await _taskService.MarkTaskCompleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}