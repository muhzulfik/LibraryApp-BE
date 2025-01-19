using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.TransaksiPeminjamanDto;
using library_be.Models;

namespace library_be.Mappers
{
    public static class TransaksiPeminjamanMappers
    {
        public static TransaksiDto ToTransaksiDto(this TransaksiPeminjaman transaksiModel)
        {
            return new TransaksiDto
            {
                IDTRANSAKSI = transaksiModel.IDTRANSAKSI,
                NIM = transaksiModel.NIM,
                TANGGALPINJAM = transaksiModel.TANGGALPINJAM,
                TANGGALKEMBALI = transaksiModel.TANGGALKEMBALI,
                STATUS = transaksiModel.STATUS,
                IDBUKU = transaksiModel.IDBUKU,
            };
        }

        public static TransaksiPeminjaman ToTransaksiFromCreateDTO(this CreateTransaksiRequestDto transaksiDto)
        {
            return new TransaksiPeminjaman
            {
                NIM = transaksiDto.NIM,
                TANGGALPINJAM = transaksiDto.TANGGALPINJAM,
                TANGGALKEMBALI = transaksiDto.TANGGALKEMBALI,
                STATUS = transaksiDto.STATUS,
                IDBUKU = transaksiDto.IDBUKU
        };
        }
    }
}
