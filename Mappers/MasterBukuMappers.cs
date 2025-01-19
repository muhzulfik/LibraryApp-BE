using library_be.Dtos.MasterBukuDto;
using library_be.Models;

namespace library_be.Mappers
{
    public static class MasterBukuMappers
    {
        public static BukuDto ToBukuDto(this MasterBuku bukuModel)
        {
            return new BukuDto
            {
                IDBUKU = bukuModel.IDBUKU,
                JUDUL = bukuModel.JUDUL,
                PENGARANG = bukuModel.PENGARANG,
                PENERBIT = bukuModel.PENERBIT,
                TAHUNTERBIT = bukuModel.TAHUNTERBIT,
            };
        }

        public static MasterBuku ToBukuFromCreateDTO(this CreateBukuRequestDto bukuDto)
        {
            return new MasterBuku
            {
                JUDUL = bukuDto.JUDUL,
                PENGARANG = bukuDto.PENGARANG,
                PENERBIT = bukuDto.PENERBIT,
                TAHUNTERBIT = bukuDto.TAHUNTERBIT,
            };
        }
    }
}
