using LibraryApi.Dtos.Books;
using LibraryApi.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/books")]
public sealed class BooksController(IBookService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookResponse>>> GetAll(
        [FromQuery] string? author,
        CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(author, cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponse>> GetById(int id, CancellationToken cancellationToken) =>
        Ok(await service.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<BookResponse>> Create(
        CreateBookRequest request,
        CancellationToken cancellationToken)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BookResponse>> Update(
        int id,
        UpdateBookRequest request,
        CancellationToken cancellationToken) =>
        Ok(await service.UpdateAsync(id, request, cancellationToken));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
