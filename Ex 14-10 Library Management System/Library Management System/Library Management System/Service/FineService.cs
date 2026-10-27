using Library_Management_System.Entities;
using Library_Management_System.Repositories;

namespace Library_Management_System.Service
{
    public interface IFineService
    {
        Task<FineResponseDto> CreateFineAsync(CreateFineDto dto);
        Task<FineResponseDto?> PayFineAsync(int fineId);
        Task<IEnumerable<FineResponseDto>> GetStudentFinesAsync(int studentId);
    }

    public class FineService : IFineService
    {
        private readonly IFineRepository _repository;

        public FineService(IFineRepository repository)
        {
            _repository = repository;
        }

        public async Task<FineResponseDto> CreateFineAsync(CreateFineDto dto)
        {
            var fine = new Fine
            {
                StudentId = dto.StudentId,
                BookId = dto.BookId,
                FineType = dto.FineType,
                IsPaid = false,
                PaymentDate = null
            };

            // Business Logic: Book Damage is always ₹0. Late Submission uses user input.
            if (dto.FineType.Equals("Book Damage", StringComparison.OrdinalIgnoreCase))
            {
                fine.Amount = 0m;
            }
            else
            {
                fine.Amount = dto.Amount ?? 0m;
            }

            var savedFine = await _repository.AddAsync(fine);
            return MapToDto(savedFine);
        }

        public async Task<FineResponseDto?> PayFineAsync(int fineId)
        {
            var fine = await _repository.GetByIdAsync(fineId);
            if (fine == null) return null;

            // Only update if it hasn't been paid yet to avoid overwriting PaymentDate
            if (!fine.IsPaid)
            {
                fine.IsPaid = true;
                fine.PaymentDate = DateTime.Now; // Automatically set to exactly when recorded
                await _repository.UpdateAsync(fine);
            }

            return MapToDto(fine);
        }

        public async Task<IEnumerable<FineResponseDto>> GetStudentFinesAsync(int studentId)
        {
            var fines = await _repository.GetByStudentIdAsync(studentId);
            return fines.Select(MapToDto).ToList();
        }

        private FineResponseDto MapToDto(Fine fine)
        {
            return new FineResponseDto
            {
                FineId = fine.Id,
                StudentId = fine.StudentId,
                BookId = fine.BookId,
                Amount = fine.Amount,
                FineType = fine.FineType,
                Status = fine.IsPaid ? "Paid" : "Pending",
                PaymentDate = fine.PaymentDate
            };
        }
    }
}