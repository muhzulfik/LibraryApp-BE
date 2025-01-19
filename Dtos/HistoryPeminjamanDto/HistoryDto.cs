using library_be.Models;

namespace library_be.Dtos.HistoryPeminjamanDto
{
    public class HistoryDto
    {
        public long IDHISTORY { get; set; }

        public long IDTRANSAKSI { get; set; }

        public string NIM { get; set; }

        public long IDBUKU { get; set; }

        public DateTime TANGGALPINJAM { get; set; }

        public DateTime TANGGALKEMBALI { get; set; }

        public int LAMAPINJAM { get; set; }

        public string STATUS {  get; set; }
        public string NamaMahasiswa { get; set; }
        public string JudulBuku { get; set; }
    }
}
