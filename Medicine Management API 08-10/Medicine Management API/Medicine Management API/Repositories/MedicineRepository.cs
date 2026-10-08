using Medicine_Management_API.Data;
using Medicine_Management_API.Entities;
using Microsoft.EntityFrameworkCore;
using Medicine_Management_API.Data;
using Medicine_Management_API.Entities;

namespace Medicine_Management_API.Repositories
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<Medicine> GetByIdAsync(Guid id);
        Task<Medicine> AddAsync(Medicine medicine);
        Task<Medicine> UpdateAsync(Medicine medicine);
        Task DeleteAsync(Medicine medicine);
    }

    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyDbContext _dbContext;

        public MedicineRepository(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Medicine>> GetAllAsync() => await _dbContext.Medicines.ToListAsync();

        public async Task<Medicine> GetByIdAsync(Guid id) => await _dbContext.Medicines.FindAsync(id);

        public async Task<Medicine> AddAsync(Medicine medicine)
        {
            await _dbContext.Medicines.AddAsync(medicine);
            await _dbContext.SaveChangesAsync();
            return medicine;
        }

        public async Task<Medicine> UpdateAsync(Medicine medicine)
        {
            _dbContext.Medicines.Update(medicine);
            await _dbContext.SaveChangesAsync();
            return medicine;
        }

        public async Task DeleteAsync(Medicine medicine)
        {
            _dbContext.Medicines.Remove(medicine);
            await _dbContext.SaveChangesAsync();
        }
    }
}