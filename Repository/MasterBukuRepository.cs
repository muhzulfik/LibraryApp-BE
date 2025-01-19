using library_be.Data;
using library_be.Dtos.MasterBukuDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Models;
using Microsoft.EntityFrameworkCore;

namespace library_be.Repository
{
    public class MasterBukuRepository : IMasterBukuRepo
    {
        private readonly ApplicationDbContext _context;

        public MasterBukuRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<MasterBuku> CreateAsync(MasterBuku masterbuku)
        {
            await _context.AddAsync(masterbuku);
            await _context.SaveChangesAsync();
            return masterbuku;
        }
        public async Task<MasterBuku?> UpdateAsync(int id, UpdateBukuRequestDto bukuDto)
        {
            var existingBuku = await _context.Masterbuku.FirstOrDefaultAsync(x => x.IDBUKU == id);

            if (existingBuku == null)
            {
                return null;
            }

            existingBuku.JUDUL = bukuDto.JUDUL;
            existingBuku.PENGARANG = bukuDto.PENGARANG;
            existingBuku.PENERBIT = bukuDto.PENERBIT;
            existingBuku.TAHUNTERBIT = bukuDto.TAHUNTERBIT;

            await _context.SaveChangesAsync();

            return existingBuku;
        }

        public async Task<MasterBuku?> DeleteAsync(int id)
        {
            var bankModel = await _context.Masterbuku.FirstOrDefaultAsync(x => x.IDBUKU == id);

            if (bankModel == null)
            {
                return null;
            }

            _context.Masterbuku.Remove(bankModel);
            await _context.SaveChangesAsync();
            return bankModel;
        }

        public async Task<MasterBuku?> GetByIdAsync(int id)
        {
            return await _context.Masterbuku.FirstOrDefaultAsync(i => i.IDBUKU == id);
        }

        public async Task<(List<MasterBuku>, int)> GetAllAsync(QueryObject query)
        {
            var bukuQuery = _context.Masterbuku.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchAll))
            {
                var searchValueLower = query.SearchAll.ToLower();
                bukuQuery = bukuQuery.Where(s =>
                    s.JUDUL.ToLower().Contains(searchValueLower) ||
                    s.PENGARANG.ToLower().Contains(searchValueLower)
                );
            }

            var totalCount = await bukuQuery.CountAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var masterbuku = await bukuQuery.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (masterbuku, totalCount);
        }
    }
}
