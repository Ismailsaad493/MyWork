using Library_Management_System.NewModule.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookMasterService
    {
        Task<IEnumerable<BookMasterResponseDto>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<BookMasterResponseDto> CreateBookAsync(BookMasterCreateDto dto);
        Task<bool> UpdateBookAsync(int id, BookMasterUpdateDto dto);
        Task<bool> DeleteBookAsync(int id);
    }

}




