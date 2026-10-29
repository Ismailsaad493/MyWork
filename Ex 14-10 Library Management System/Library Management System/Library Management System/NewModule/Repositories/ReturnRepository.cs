using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Repositories
{
    public interface IReturnRepository
    {
        Task<IEnumerable<ReturnMaster>> GetAllAsync();
        Task<ReturnMaster?> GetByIdAsync(string id);
        Task<ReturnMaster> AddAsync(ReturnMaster returnMaster);
        Task UpdateAsync(ReturnMaster returnMaster);
    }

    public class ReturnRepository : IReturnRepository
    {
        private readonly NewBooksDbContext _context;

        public ReturnRepository(NewBooksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReturnMaster>> GetAllAsync()
        {
            return await _context.ReturnMasters.ToListAsync();
        }

        public async Task<ReturnMaster?> GetByIdAsync(string id)
        {
            return await _context.ReturnMasters.FindAsync(id);
        }

        public async Task<ReturnMaster> AddAsync(ReturnMaster returnMaster)
        {
            await _context.ReturnMasters.AddAsync(returnMaster);
            await _context.SaveChangesAsync();
            return returnMaster;
        }

        public async Task UpdateAsync(ReturnMaster returnMaster)
        {
            _context.ReturnMasters.Update(returnMaster);
            await _context.SaveChangesAsync();
        }
    }
}