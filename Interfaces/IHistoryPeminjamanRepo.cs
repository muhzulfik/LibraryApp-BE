using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Helper;
using library_be.Models;

namespace library_be.Interfaces
{
    public interface IHistoryPeminjamanRepo
    {
        Task<HistoryPeminjaman> CreateAsync(HistoryPeminjaman historypeminjaman);
        Task<HistoryPeminjaman?> UpdateAsync(int id, UpdateHistoryRequestDto historyDto);
        Task<HistoryPeminjaman?> DeleteAsync(int id);
        Task<HistoryPeminjaman?> GetByIdAsync(int id);
        Task<(List<HistoryPeminjaman>, int)> GetAllAsync(QueryObject query);
    }
}
