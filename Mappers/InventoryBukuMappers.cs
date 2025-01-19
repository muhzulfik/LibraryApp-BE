using library_be.Dtos.InventoryBukuDto;
using library_be.Dtos.MasterBukuDto;
using library_be.Dtos.MasterMahasiswaDto;
using library_be.Models;

namespace library_be.Mappers
{
    public static class InventoryBukuMappers
    {
        public static InventoryDto ToInventoryDto(this InventoryBuku inventoryModel)
        {
            return new InventoryDto
            {
                IDSTOK = inventoryModel.IDSTOK,
                IDBUKU = inventoryModel.IDBUKU,
                LOKASIRAK = inventoryModel.LOKASIRAK,
                JUMLAHSTOK = inventoryModel.JUMLAHSTOK,
                JudulBuku = inventoryModel.MasterBuku?.JUDUL,
            };
        }

        public static InventoryBuku ToInventoryFromCreateDTO(this CreateInventoryRequestDto inventoryDto)
        {
            return new InventoryBuku
            {
                IDBUKU = inventoryDto.IDBUKU,
                LOKASIRAK = inventoryDto.LOKASIRAK,
                JUMLAHSTOK = inventoryDto.JUMLAHSTOK,
            };
        }
    }
}
