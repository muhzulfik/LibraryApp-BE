using library_be.Dtos.MasterBukuDto;
using library_be.Helper;
using library_be.Models;

namespace library_be.Interfaces
{
    public interface IMasterBukuRepo
    {
        Task<MasterBuku> CreateAsync(MasterBuku masterbuku);
        Task<MasterBuku?> UpdateAsync(int id, UpdateBukuRequestDto bukuDto);
        Task<MasterBuku?> DeleteAsync(int id);
        Task<MasterBuku?> GetByIdAsync(int id);
        Task<(List<MasterBuku>, int)> GetAllAsync(QueryObject query);
    }
}
