using Book_Library_API.Entities;
using Book_Library_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Book_Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAuthorDto dto)
        {
            var createdAuthor = await _authorService.CreateAuthorAsync(dto);
            return Ok(createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthorDto dto)
        {
            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, dto);
            if (updatedAuthor == null) return NotFound();
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _authorService.DeleteAuthorAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent();
        }
    }
}