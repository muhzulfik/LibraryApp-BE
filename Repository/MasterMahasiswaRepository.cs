using library_be.Data;
using library_be.Dtos.MasterBukuDto;
using library_be.Dtos.MasterMahasiswaDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace library_be.Repository
{
    public class MasterMahasiswaRepository : IMasterMahasiswaRepo
    {
        private readonly ApplicationDbContext _context;

        public MasterMahasiswaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MasterMahasiswa> CreateAsync(MasterMahasiswa mastermahasiswa)
        {
            await _context.AddAsync(mastermahasiswa);
            await _context.SaveChangesAsync();
            return mastermahasiswa;
        }
        public async Task<MasterMahasiswa?> UpdateAsync(string id, UpdateMahasiswaRequestDto mahasiswaDto)
        {
            var existingMahasiswa = await _context.Mastermahasiswa.FirstOrDefaultAsync(x => x.NIM == id);

            if (existingMahasiswa == null)
            {
                return null;
            }

            existingMahasiswa.NAMA = mahasiswaDto.NAMA;
            existingMahasiswa.FAKULTAS = mahasiswaDto.FAKULTAS;
            existingMahasiswa.JURUSAN = mahasiswaDto.JURUSAN;
            existingMahasiswa.STATUS = mahasiswaDto.STATUS;

            await _context.SaveChangesAsync();

            return existingMahasiswa;
        }

        public async Task<MasterMahasiswa?> DeleteAsync(string id)
        {
            var mahasiswaModel = await _context.Mastermahasiswa.FirstOrDefaultAsync(x => x.NIM == id);

            if (mahasiswaModel == null)
            {
                return null;
            }

            _context.Mastermahasiswa.Remove(mahasiswaModel);
            await _context.SaveChangesAsync();
            return mahasiswaModel;
        }

        public async Task<MasterMahasiswa?> GetByIdAsync(string id)
        {
            return await _context.Mastermahasiswa.FirstOrDefaultAsync(i => i.NIM == id);
        }

        public async Task<(List<MasterMahasiswa>, int)> GetAllAsync(QueryObject query)
        {
            var mahasiswaQuery = _context.Mastermahasiswa.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchAll))
            {
                var searchValueLower = query.SearchAll.ToLower();
                mahasiswaQuery = mahasiswaQuery.Where(s =>
                    s.NAMA.ToLower().Contains(searchValueLower) ||
                    s.JURUSAN.ToLower().Contains(searchValueLower)
                );
            }

            var totalCount = await mahasiswaQuery.CountAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var mahasiswa = await mahasiswaQuery.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (mahasiswa, totalCount);
        }
    }
}
