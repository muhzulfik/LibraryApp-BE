using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_be.Models
{
    public class HistoryPeminjaman
    {
        [Key]
        public long IDHISTORY { get; set; }

        public long? IDTRANSAKSI { get; set; }

        public string NIM { get; set; }

        public long IDBUKU { get; set; }

        public DateTime TANGGALPINJAM { get; set; }

        public DateTime TANGGALKEMBALI { get; set; }

        public int LAMAPINJAM { get; set; }

        public MasterMahasiswa MasterMahasiswa { get; set; }
        public MasterBuku MasterBuku { get; set; }

        [ForeignKey("IDTRANSAKSI")]
        public TransaksiPeminjaman TransaksiPeminjaman { get; set; }
    }
}
