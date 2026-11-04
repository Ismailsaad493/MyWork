using Library_Management_System.NewModule.Dtos;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Controllers
{
    [ApiController]
    [Route("api/new-books-module/issue-master")]
    public class IssueMasterController : ControllerBase
    {
        private readonly IIssueMasterService _service;

        public IssueMasterController(IIssueMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                return Ok(await _service.GetAllIssuesAsync(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueCreateDto dto)
        {
            try
            {
                var result = await _service.CreateIssueAsync(dto);
                if (result == null)
                {
                    return StatusCode(403, new { Message = "invalid student id or book copy not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] IssueUpdateDto dto)
        {
            try
            {
                var success = await _service.UpdateIssueAsync(id, dto);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid issue id or student id not found" });
                }
                return Ok(new { Message = "Issue updated successfully." });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var success = await _service.DeleteIssueAsync(id);
                if (!success)
                {
                    return StatusCode(403, new { Message = "invalid issue id not found" });
                }
                return Ok(new { Message = "Issue deleted successfully." });
            }
            catch (Exception ex)
            {
                var actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { Message = actualError });
            }
        }
    }
}