using Medicine_Management_API.Entities;
using Medicine_Management_API.Models;
using Medicine_Management_API.Repositories;
using Medicine_Management_API.Entities;
using Medicine_Management_API.Models;
using Medicine_Management_API.Repositories;

namespace Medicine_Management_API.Services
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineResponseDto>> GetAllMedicinesAsync();
        Task<MedicineResponseDto> GetMedicineByIdAsync(Guid id);
        Task<MedicineResponseDto> CreateMedicineAsync(MedicineRequestDto dto);
        Task<MedicineResponseDto> UpdateMedicineAsync(Guid id, MedicineRequestDto dto);
        Task<bool> DeleteMedicineAsync(Guid id);
    }

    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;

        public MedicineService(IMedicineRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MedicineResponseDto>> GetAllMedicinesAsync()
        {
            var medicines = await _repository.GetAllAsync();
            return medicines.Select(m => new MedicineResponseDto
            {
                Id = m.Id,
                Name = m.Name,
                Manufacturer = m.Manufacturer
            });
        }

        public async Task<MedicineResponseDto> GetMedicineByIdAsync(Guid id)
        {
            var medicine = await _repository.GetByIdAsync(id);
            if (medicine == null) return null;

            return new MedicineResponseDto { Id = medicine.Id, Name = medicine.Name, Manufacturer = medicine.Manufacturer };
        }

        public async Task<MedicineResponseDto> CreateMedicineAsync(MedicineRequestDto dto)
        {
            var entity = new Medicine { Id = Guid.NewGuid(), Name = dto.Name, Manufacturer = dto.Manufacturer };
            var savedEntity = await _repository.AddAsync(entity);
            return new MedicineResponseDto { Id = savedEntity.Id, Name = savedEntity.Name, Manufacturer = savedEntity.Manufacturer };
        }

        public async Task<MedicineResponseDto> UpdateMedicineAsync(Guid id, MedicineRequestDto dto)
        {
            var existingMedicine = await _repository.GetByIdAsync(id);
            if (existingMedicine == null) return null;

            existingMedicine.Name = dto.Name;
            existingMedicine.Manufacturer = dto.Manufacturer;

            var updatedEntity = await _repository.UpdateAsync(existingMedicine);
            return new MedicineResponseDto { Id = updatedEntity.Id, Name = updatedEntity.Name, Manufacturer = updatedEntity.Manufacturer };
        }

        public async Task<bool> DeleteMedicineAsync(Guid id)
        {
            var existingMedicine = await _repository.GetByIdAsync(id);
            if (existingMedicine == null) return false;

            await _repository.DeleteAsync(existingMedicine);
            return true;
        }
    }
}