using library_be.Dtos.MasterBukuDto;
using library_be.Dtos.MasterMahasiswaDto;
using library_be.Models;

namespace library_be.Mappers
{
    public static class MasterMahasiswaMappers
    {
        public static MahasiswaDto ToMahasiswaDto(this MasterMahasiswa mahasiswaModel)
        {
            return new MahasiswaDto
            {
                NIM = mahasiswaModel.NIM,
                NAMA = mahasiswaModel.NAMA,
                FAKULTAS = mahasiswaModel.FAKULTAS,
                JURUSAN = mahasiswaModel.JURUSAN,
                STATUS = mahasiswaModel.STATUS,
            };
        }

        public static MasterMahasiswa ToMahasiswaFromCreateDTO(this CreateMahasiswaRequestDto mahasiswaDto)
        {
            return new MasterMahasiswa
            {
                NIM = mahasiswaDto.NIM,
                NAMA = mahasiswaDto.NAMA,
                FAKULTAS = mahasiswaDto.FAKULTAS,
                JURUSAN = mahasiswaDto.JURUSAN,
                STATUS = mahasiswaDto.STATUS,
            };
        }
    }
}
