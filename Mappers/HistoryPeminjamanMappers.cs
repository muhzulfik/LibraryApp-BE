using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.InventoryBukuDto;
using library_be.Models;

namespace library_be.Mappers
{
    public static class HistoryPeminjamanMappers
    {
        public static HistoryDto ToHistoryDto(this HistoryPeminjaman historyModel)
        {
            return new HistoryDto
            {
                IDBUKU = historyModel.IDBUKU,
                NIM = historyModel.NIM,
                TANGGALPINJAM = historyModel.TANGGALPINJAM,
                TANGGALKEMBALI = historyModel.TANGGALKEMBALI,
                LAMAPINJAM = historyModel.LAMAPINJAM,
                IDHISTORY = historyModel.IDHISTORY,
                NamaMahasiswa = historyModel.MasterMahasiswa?.NAMA,
                JudulBuku = historyModel.MasterBuku?.JUDUL,
                STATUS = historyModel.TransaksiPeminjaman?.STATUS
            };
        }

        public static HistoryPeminjaman ToHistoryFromCreateDTO(this CreateHistoryRequestDto historyDto)
        {
            return new HistoryPeminjaman
            {
                IDTRANSAKSI = historyDto.IDTRANSAKSI,
                IDBUKU = historyDto.IDBUKU,
                NIM = historyDto.NIM,
                TANGGALPINJAM = historyDto.TANGGALPINJAM,
                TANGGALKEMBALI = historyDto.TANGGALKEMBALI,
                LAMAPINJAM = historyDto.LAMAPINJAM,
            };
        }
    }
}
