using Medicine_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Medicine_Management_API.Models;
using Medicine_Management_API.Services;

namespace Medicine_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // 1. READ ALL (GET)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicineService.GetAllMedicinesAsync();
            return Ok(result);
        }

        // 2. READ ONE (GET)
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _medicineService.GetMedicineByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 3. CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(MedicineRequestDto request)
        {
            var result = await _medicineService.CreateMedicineAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // 4. UPDATE (PUT)
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, MedicineRequestDto request)
        {
            var result = await _medicineService.UpdateMedicineAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 5. DELETE (DELETE)
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _medicineService.DeleteMedicineAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent(); // 204 No Content is standard for successful deletions
        }
    }
}