using library_be.Dtos.MasterMahasiswaDto;
using library_be.Helper;
using library_be.Models;

namespace library_be.Interfaces
{
    public interface IMasterMahasiswaRepo
    {
        Task<MasterMahasiswa> CreateAsync(MasterMahasiswa mastermahasiswa);
        Task<MasterMahasiswa?> UpdateAsync(string id, UpdateMahasiswaRequestDto mahasiswaDto);
        Task<MasterMahasiswa?> DeleteAsync(string id);
        Task<MasterMahasiswa?> GetByIdAsync(string id);
        Task<(List<MasterMahasiswa>, int)> GetAllAsync(QueryObject query);
    }
}
