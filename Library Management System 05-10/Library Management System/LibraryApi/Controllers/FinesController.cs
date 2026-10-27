using LibraryApi.Dtos.Fines;
using LibraryApi.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/fines")]
public sealed class FinesController(IFineService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FineResponse>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FineResponse>> GetById(int id, CancellationToken cancellationToken) =>
        Ok(await service.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<FineResponse>> Create(
        CreateFineRequest request,
        CancellationToken cancellationToken)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPost("{id:int}/payment")]
    public async Task<ActionResult<FineResponse>> RecordPayment(
        int id,
        RecordFinePaymentRequest request,
        CancellationToken cancellationToken) =>
        Ok(await service.RecordPaymentAsync(id, request, cancellationToken));
}
