using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IFineMasterRepository
    {
        Task<IEnumerable<FineMaster>> GetAllAsync();
        Task<FineMaster?> GetByIdAsync(string id);
        Task<FineMaster> AddAsync(FineMaster fineMaster);
        Task UpdateAsync(FineMaster fineMaster);
    }

    public class FineMasterRepository : IFineMasterRepository
    {
        private readonly NewBooksDbContext _context;

        public FineMasterRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FineMaster>> GetAllAsync()
        {
            return await _context.FineMasters.ToListAsync();
        }

        public async Task<FineMaster?> GetByIdAsync(string id)
        {
            return await _context.FineMasters.FindAsync(id);
        }

        public async Task<FineMaster> AddAsync(FineMaster fineMaster)
        {
            await _context.FineMasters.AddAsync(fineMaster);
            await _context.SaveChangesAsync();
            return fineMaster;
        }

        public async Task UpdateAsync(FineMaster fineMaster)
        {
            _context.FineMasters.Update(fineMaster);
            await _context.SaveChangesAsync();
        }
    }
}