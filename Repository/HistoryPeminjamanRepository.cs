using library_be.Data;
using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.InventoryBukuDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace library_be.Repository
{
    public class HistoryPeminjamanRepository : IHistoryPeminjamanRepo
    {
        private readonly ApplicationDbContext _context;

        public HistoryPeminjamanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HistoryPeminjaman> CreateAsync(HistoryPeminjaman historypeminjaman)
        {
            await _context.AddAsync(historypeminjaman);
            await _context.SaveChangesAsync();
            return historypeminjaman;
        }
        public async Task<HistoryPeminjaman?> UpdateAsync(int id, UpdateHistoryRequestDto historyDto)
        {
            var existingHistory = await _context.Historypeminjaman.FirstOrDefaultAsync(x => x.IDHISTORY == id);

            if (existingHistory == null)
            {
                return null;
            }

            existingHistory.IDTRANSAKSI = historyDto.IDTRANSAKSI;
            existingHistory.IDBUKU = historyDto.IDBUKU;
            existingHistory.NIM = historyDto.NIM;
            existingHistory.TANGGALPINJAM = historyDto.TANGGALPINJAM;
            existingHistory.TANGGALKEMBALI = historyDto.TANGGALKEMBALI;
            existingHistory.LAMAPINJAM = historyDto.LAMAPINJAM;

            await _context.SaveChangesAsync();

            return existingHistory;
        }

        public async Task<HistoryPeminjaman?> DeleteAsync(int id)
        {
            var historyModel = await _context.Historypeminjaman.FirstOrDefaultAsync(x => x.IDHISTORY == id);

            if (historyModel == null)
            {
                return null;
            }

            _context.Historypeminjaman.Remove(historyModel);
            await _context.SaveChangesAsync();
            return historyModel;
        }

        public async Task<HistoryPeminjaman?> GetByIdAsync(int id)
        {
            return await _context.Historypeminjaman.FirstOrDefaultAsync(i => i.IDHISTORY == id);
        }

        public async Task<(List<HistoryPeminjaman>, int)> GetAllAsync(QueryObject query)
        {
            var historyQuery = _context.Historypeminjaman
                .Include(hp => hp.MasterMahasiswa)
                .Include(hp => hp.MasterBuku)
                .Include(hp => hp.TransaksiPeminjaman)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(query.SearchAll))
            {
                historyQuery = historyQuery.Where(s =>
                    s.NIM.ToString().Contains(query.SearchAll)
                );
            }

            var totalCount = await historyQuery.CountAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var history = await historyQuery.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (history, totalCount);
        }
    }
}
