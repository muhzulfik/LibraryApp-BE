using library_be.Data;
using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.TransaksiPeminjamanDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace library_be.Repository
{
    public class TransaksiPeminjamanRepository : ITransaksiPeminjamanRepo
    {
        private readonly ApplicationDbContext _context;

        public TransaksiPeminjamanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransaksiPeminjaman> CreateAsync(TransaksiPeminjaman transaksiPeminjaman)
        {

            await _context.AddAsync(transaksiPeminjaman);

            await _context.SaveChangesAsync();

            foreach (var idBuku in transaksiPeminjaman.IDBUKU)
            {
                var stokBuku = await _context.Inventorybuku.FirstOrDefaultAsync(r => r.IDBUKU == idBuku);
                if (stokBuku != null)
                {
                    stokBuku.JUMLAHSTOK -= 1;
                }

                var historyPeminjaman = new HistoryPeminjaman
                {
                    IDTRANSAKSI = transaksiPeminjaman.IDTRANSAKSI,
                    NIM = transaksiPeminjaman.NIM,
                    IDBUKU = idBuku,
                    TANGGALPINJAM = transaksiPeminjaman.TANGGALPINJAM,
                    TANGGALKEMBALI = transaksiPeminjaman.TANGGALKEMBALI,
                    LAMAPINJAM = (transaksiPeminjaman.TANGGALKEMBALI - transaksiPeminjaman.TANGGALPINJAM).Days
                };

                await _context.AddAsync(historyPeminjaman);
            }

            await _context.SaveChangesAsync();

            return transaksiPeminjaman;
        }

        public async Task<TransaksiPeminjaman?> UpdateAsync(int id, UpdateTransaksiRequestDto transaksiDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingTransaksi = await _context.Transaksipeminjaman
                                                      .FirstOrDefaultAsync(x => x.IDTRANSAKSI == id);

                if (existingTransaksi == null)
                {
                    return null;
                }

                // Update existing transaction details
                existingTransaksi.NIM = transaksiDto.NIM;
                existingTransaksi.TANGGALPINJAM = transaksiDto.TANGGALPINJAM;
                existingTransaksi.TANGGALKEMBALI = transaksiDto.TANGGALKEMBALI;
                existingTransaksi.STATUS = transaksiDto.STATUS;
                existingTransaksi.IDBUKU = transaksiDto.IDBUKU;

                await _context.SaveChangesAsync();

                // Add new history records based on updated book IDs
                foreach (var idBuku in transaksiDto.IDBUKU)
                {
                    var stokBuku = await _context.Inventorybuku.FirstOrDefaultAsync(r => r.IDBUKU == idBuku);
                    if (stokBuku != null && existingTransaksi.STATUS.Contains("Dikembalikan"))
                    {
                        stokBuku.JUMLAHSTOK += 1;
                    }
                    if (stokBuku != null && existingTransaksi.STATUS.Contains("Dipinjam"))
                    {
                        stokBuku.JUMLAHSTOK -= 1;
                    }
                    var historyPeminjaman = new HistoryPeminjaman
                    {
                        IDTRANSAKSI = existingTransaksi.IDTRANSAKSI,
                        NIM = transaksiDto.NIM,
                        IDBUKU = idBuku,
                        TANGGALPINJAM = transaksiDto.TANGGALPINJAM,
                        TANGGALKEMBALI = transaksiDto.TANGGALKEMBALI,
                        LAMAPINJAM = (transaksiDto.TANGGALKEMBALI - transaksiDto.TANGGALPINJAM).Days
                    };

                    await _context.AddAsync(historyPeminjaman);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return existingTransaksi;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<TransaksiPeminjaman?> DeleteAsync(int id)
        {
            var transaksiModel = await _context.Transaksipeminjaman.FirstOrDefaultAsync(x => x.IDTRANSAKSI == id);

            if (transaksiModel == null)
            {
                return null;
            }

            _context.Transaksipeminjaman.Remove(transaksiModel);
            await _context.SaveChangesAsync();

            foreach (var idBuku in transaksiModel.IDBUKU)
            {
                var stokBuku = await _context.Inventorybuku.FirstOrDefaultAsync(r => r.IDBUKU == idBuku);
                if (stokBuku != null && transaksiModel.STATUS.Contains("Dipinjam"))
                {
                    stokBuku.JUMLAHSTOK += 1;
                }
            }
            return transaksiModel;
        }

        public async Task<TransaksiPeminjaman?> GetByIdAsync(int id)
        {
            return await _context.Transaksipeminjaman.FirstOrDefaultAsync(i => i.IDTRANSAKSI == id);
        }

        public async Task<(List<TransaksiPeminjaman>, int)> GetAllAsync(QueryObject query)
        {
            var transaksiQuery = _context.Transaksipeminjaman.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchAll))
            {
                var searchAll = query.SearchAll.ToLower();

                transaksiQuery = transaksiQuery.Where(s =>
                    s.NIM.ToString().Contains(searchAll) ||
                    s.MasterMahasiswa.NAMA.ToLower().Contains(searchAll) ||
                    s.IDBUKU.ToString().Contains(searchAll) ||
                    s.TANGGALPINJAM.ToString("yyyy-MM-dd").Contains(searchAll) ||
                    s.TANGGALKEMBALI.ToString("yyyy-MM-dd").Contains(searchAll)
                );
            }

            var totalCount = await transaksiQuery.CountAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var transaksi = await transaksiQuery.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (transaksi, totalCount);
        }
    }
}
