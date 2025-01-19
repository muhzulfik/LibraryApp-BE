using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_be.Models
{
    public class TransaksiPeminjaman
    {
        [Key]
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
