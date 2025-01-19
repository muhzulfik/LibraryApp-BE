using System.ComponentModel.DataAnnotations;

namespace library_be.Models
{
    public class MasterMahasiswa
    {
        [Key]
        public string NIM { get; set; }

        [MaxLength(200)]
        public string NAMA { get; set; }

        [MaxLength(200)]
        public string FAKULTAS { get; set; }

        [MaxLength(200)]
        public string JURUSAN { get; set; }

        [MaxLength(10)]
        public string STATUS { get; set; }

        // Navigation property
        public ICollection<TransaksiPeminjaman> TransaksiPeminjaman { get; set; }
        public ICollection<HistoryPeminjaman> HistoryPeminjaman { get; set; }
    }
}
