using library_be.Models;
using System.ComponentModel.DataAnnotations;

namespace library_be.Dtos.TransaksiPeminjamanDto
{
    public class TransaksiDto
    {
        public long IDTRANSAKSI { get; set; }

        public string NIM { get; set; }

        public DateTime TANGGALPINJAM { get; set; }

        public DateTime TANGGALKEMBALI { get; set; }

        [MaxLength(100)]
        public string STATUS { get; set; }

        public List<int> IDBUKU { get; set; }

        public MasterMahasiswa MasterMahasiswa { get; set; }
        public ICollection<HistoryPeminjaman> HistoryPeminjaman { get; set; } = new List<HistoryPeminjaman>();
    }
}
