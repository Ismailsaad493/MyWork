using Library_Management_System.NewModule.Dtos;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookMasterService
    {
        Task<IEnumerable<BookMasterResponseDto>> GetAllBooksAsync();
        Task<BookMasterResponseDto> CreateBookAsync(BookMasterCreateDto dto);
    }
}