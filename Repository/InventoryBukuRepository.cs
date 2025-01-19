using library_be.Data;
using library_be.Dtos.InventoryBukuDto;
using library_be.Dtos.MasterMahasiswaDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace library_be.Repository
{
    public class InventoryBukuRepository : IInventoryBukuRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryBukuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryBuku> CreateAsync(InventoryBuku inventorybuku)
        {
            await _context.AddAsync(inventorybuku);
            await _context.SaveChangesAsync();
            return inventorybuku;
        }
        public async Task<InventoryBuku?> UpdateAsync(int id, UpdateInventoryRequestDto inventoryDto)
        {
            var existingInventory = await _context.Inventorybuku.FirstOrDefaultAsync(x => x.IDSTOK == id);

            if (existingInventory == null)
            {
                return null;
            }

            existingInventory.IDBUKU = inventoryDto.IDBUKU;
            existingInventory.LOKASIRAK = inventoryDto.LOKASIRAK;
            existingInventory.JUMLAHSTOK = inventoryDto.JUMLAHSTOK;

            await _context.SaveChangesAsync();

            return existingInventory;
        }

        public async Task<InventoryBuku?> DeleteAsync(int id)
        {
            var inventoryModel = await _context.Inventorybuku.FirstOrDefaultAsync(x => x.IDSTOK == id);

            if (inventoryModel == null)
            {
                return null;
            }

            _context.Inventorybuku.Remove(inventoryModel);
            await _context.SaveChangesAsync();
            return inventoryModel;
        }

        public async Task<InventoryBuku?> GetByIdAsync(int id)
        {
            return await _context.Inventorybuku.FirstOrDefaultAsync(i => i.IDSTOK == id);
        }

        public async Task<(List<InventoryBuku>, int)> GetAllAsync(QueryObject query)
        {
            var inventoryQuery = _context.Inventorybuku.Include(a => a.MasterBuku).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchAll))
            {
                var searchValueLower = query.SearchAll.ToLower();
                inventoryQuery = inventoryQuery.Where(s =>
                    s.LOKASIRAK.ToLower().Contains(searchValueLower)
                );
            }

            var totalCount = await inventoryQuery.CountAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var inventory = await inventoryQuery.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (inventory, totalCount);
        }
    }
}
