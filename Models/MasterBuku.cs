using System.ComponentModel.DataAnnotations;

namespace library_be.Models
{
    public class MasterBuku
    {
        [Key]
        public long IDBUKU { get; set; }

        [MaxLength(200)]
        public string JUDUL { get; set; }

        [MaxLength(200)]
        public string PENGARANG { get; set; }

        [MaxLength(200)]
        public string? PENERBIT { get; set; }

        public string? TAHUNTERBIT { get; set; }

        public ICollection<InventoryBuku> InventoryBuku { get; set; }
        public ICollection<HistoryPeminjaman> HistoryPeminjaman { get; set; }
    }
}
