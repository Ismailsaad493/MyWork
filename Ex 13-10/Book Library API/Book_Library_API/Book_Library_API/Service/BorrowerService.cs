using Book_Library_API.Entities;
using Book_Library_API.Repositories;

namespace Book_Library_API.Service
{
    public interface IBorrowerService
    {
        Task<IEnumerable<BorrowerDto>> GetAllBorrowersAsync();
        Task<BorrowerDto> CreateBorrowerAsync(AddBorrowerDto dto);
    }

    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _repository;

        public BorrowerService(IBorrowerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BorrowerDto>> GetAllBorrowersAsync()
        {
            var borrowers = await _repository.GetAllAsync();
            return borrowers.Select(b => new BorrowerDto { Id = b.Id, Name = b.Name, Email = b.Email });
        }

        public async Task<BorrowerDto> CreateBorrowerAsync(AddBorrowerDto dto)
        {
            var borrower = new Borrower { Name = dto.Name, Email = dto.Email };
            var savedBorrower = await _repository.AddAsync(borrower);
            return new BorrowerDto { Id = savedBorrower.Id, Name = savedBorrower.Name, Email = savedBorrower.Email };
        }
    }
}