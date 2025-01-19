using library_be.Dtos.TransaksiPeminjamanDto;
using library_be.Helper;
using library_be.Models;

namespace library_be.Interfaces
{
    public interface ITransaksiPeminjamanRepo
    {
        Task<TransaksiPeminjaman> CreateAsync(TransaksiPeminjaman transaksiPeminjaman);
        Task<TransaksiPeminjaman?> UpdateAsync(int id, UpdateTransaksiRequestDto transaksiDto);
        Task<TransaksiPeminjaman?> DeleteAsync(int id);
        Task<TransaksiPeminjaman?> GetByIdAsync(int id);
        Task<(List<TransaksiPeminjaman>, int)> GetAllAsync(QueryObject query);
    }
}
