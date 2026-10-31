using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IFineDetailMasterRepository
    {
        Task<IEnumerable<FineDetailMaster>> GetAllAsync();
        Task<FineDetailMaster?> GetByIdAsync(string id);
        Task<IEnumerable<FineDetailMaster>> GetByFineIdAsync(string fineId);
        Task<FineDetailMaster> AddAsync(FineDetailMaster fineDetail);
        Task UpdateAsync(FineDetailMaster fineDetail);
    }

    public class FineDetailMasterRepository : IFineDetailMasterRepository
    {
        private readonly NewBooksDbContext _context;

        public FineDetailMasterRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FineDetailMaster>> GetAllAsync()
        {
            return await _context.FineDetails.ToListAsync();
        }

        public async Task<FineDetailMaster?> GetByIdAsync(string id)
        {
            return await _context.FineDetails.FindAsync(id);
        }

        public async Task<IEnumerable<FineDetailMaster>> GetByFineIdAsync(string fineId)
        {
            return await _context.FineDetails.Where(fd => fd.FineId == fineId).ToListAsync();
        }

        public async Task<FineDetailMaster> AddAsync(FineDetailMaster fineDetail)
        {
            await _context.FineDetails.AddAsync(fineDetail);
            await _context.SaveChangesAsync();
            return fineDetail;
        }

        public async Task UpdateAsync(FineDetailMaster fineDetail)
        {
            _context.FineDetails.Update(fineDetail);
            await _context.SaveChangesAsync();
        }
    }
}