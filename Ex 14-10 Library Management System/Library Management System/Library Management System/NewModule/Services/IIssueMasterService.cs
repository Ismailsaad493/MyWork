using Library_Management_System.NewModule.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Services
{
    public interface IIssueMasterService
    {
        Task<IEnumerable<IssueResponseDto>> GetAllIssuesAsync(int pageNumber, int pageSize);
        Task<IssueResponseDto> CreateIssueAsync(IssueCreateDto dto);
        Task<bool> UpdateIssueAsync(string id, IssueUpdateDto dto);
        Task<bool> DeleteIssueAsync(string id);
    }
}