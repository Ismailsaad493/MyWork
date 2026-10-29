using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/issue-detail-master")]
    public class IssueDetailMasterController : ControllerBase
    {
        private readonly IIssueDetailService _service;

        public IssueDetailMasterController(IIssueDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllDetailsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDetailCreateDto dto)
        {
            var result = await _service.CreateDetailAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.IssueDetailId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDetailUpdateDto dto)
        {
            var success = await _service.UpdateDetailAsync(id, dto);
            if (!success) return NotFound($"Issue Detail with ID {id} not found.");

            return NoContent();
        }
    }
}