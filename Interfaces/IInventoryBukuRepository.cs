using library_be.Dtos.InventoryBukuDto;
using library_be.Helper;
using library_be.Models;

namespace library_be.Interfaces
{
    public interface IInventoryBukuRepository
    {
        Task<InventoryBuku> CreateAsync(InventoryBuku inventorybuku);
        Task<InventoryBuku?> UpdateAsync(int id, UpdateInventoryRequestDto inventoryDto);
        Task<InventoryBuku?> DeleteAsync(int id);
        Task<InventoryBuku?> GetByIdAsync(int id);
        Task<(List<InventoryBuku>, int)> GetAllAsync(QueryObject query);

    }
}
