using Library_Management_System.NewModule.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IBookMasterService
    {
        Task<IEnumerable<BookMasterResponseDto>> GetAllBooksAsync();

        // 2. For POST
        Task<BookMasterResponseDto> CreateBookAsync(BookMasterCreateDto dto);

        // 3. For PUT
        Task<bool> UpdateBookAsync(int id, BookMasterUpdateDto dto);
    }

}




