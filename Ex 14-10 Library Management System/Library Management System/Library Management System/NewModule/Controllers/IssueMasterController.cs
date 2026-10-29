using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/issue-master")]
    public class IssueMasterController : ControllerBase
    {
        private readonly IIssueService _service;

        public IssueMasterController(IIssueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllIssuesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueCreateDto dto)
        {
            var result = await _service.CreateIssueAsync(dto);
            // Assuming your CreateIssueAsync returns the created IssueMaster object
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] IssueUpdateDto dto)
        {
            var success = await _service.UpdateIssueAsync(id, dto);
            if (!success) return NotFound($"Issue with ID {id} not found.");

            return NoContent();
        }
    }
}