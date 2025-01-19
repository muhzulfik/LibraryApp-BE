namespace library_be.Dtos.HistoryPeminjamanDto
{
    public class UpdateHistoryRequestDto
    {
        public long IDTRANSAKSI { get; set; }

        public string NIM { get; set; }

        public long IDBUKU { get; set; }

        public DateTime TANGGALPINJAM { get; set; }

        public DateTime TANGGALKEMBALI { get; set; }

        public int LAMAPINJAM { get; set; }
    }
}
