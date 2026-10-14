using Book_Library_API.Entities;
using Book_Library_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Book_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var borrowers = await _borrowerService.GetAllBorrowersAsync();
            return Ok(borrowers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBorrowerDto dto)
        {
            var createdBorrower = await _borrowerService.CreateBorrowerAsync(dto);
            return Ok(createdBorrower);
        }
    }
}