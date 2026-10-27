using LibraryApi.Dtos.Loans;
using LibraryApi.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/loans")]
public sealed class LoansController(ILoanService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanResponse>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LoanResponse>> GetById(int id, CancellationToken cancellationToken) =>
        Ok(await service.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<LoanResponse>> Checkout(
        CheckoutLoanRequest request,
        CancellationToken cancellationToken)
    {
        var response = await service.CheckoutAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPost("{id:int}/return")]
    public async Task<ActionResult<LoanResponse>> Return(int id, CancellationToken cancellationToken) =>
        Ok(await service.ReturnAsync(id, cancellationToken));
}
