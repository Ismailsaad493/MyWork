using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/issue-detail")]
    public class IssueDetailController : ControllerBase
    {
        private readonly IIssueDetailService _service;

        public IssueDetailController(IIssueDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _service.GetAllDetailsAsync(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail(string issueId, [FromBody] IssueDetailCreateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            var result = await _service.CreateDetailAsync(issueId, dto);
            var cleanResponse = new IssueDetailResponseDto
            {
                IssueDetailId = result.IssueDetailId,
                IssueId = result.IssueId,
                BookCopyId = result.BookCopyId,
                BookTitle = "Will populate on GET",
                DueDate = result.DueDate,
                Status = result.Status
            };

            return CreatedAtAction(nameof(GetAll), new { id = result.IssueDetailId }, cleanResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDetailUpdateDto dto)
        {
            if (dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest(new { Message = "type only 1 or 2" });
            }
            try
            {
                var result = await _service.UpdateDetailAsync(id, dto);

                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                var cleanResponse = new IssueDetailResponseDto
                {
                    IssueDetailId = result.IssueDetailId,
                    IssueId = result.IssueId,
                    BookCopyId = result.BookCopyId,
                    BookTitle = "Will populate on GET",
                    DueDate = result.DueDate,
                    Status = result.Status
                };

                return Ok(cleanResponse);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteDetailAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid id or id not found" });
                }

                return Ok(new { Message = "Issue detail deleted successfully." });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "An error occurred while processing your request." });
            }
        }
    }
} 