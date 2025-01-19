using library_be.Models;
using System.ComponentModel.DataAnnotations;

namespace library_be.Dtos.MasterMahasiswaDto
{
    public class MahasiswaDto
    {
        public string NIM { get; set; }

        public string NAMA { get; set; }

        public string FAKULTAS { get; set; }

        public string JURUSAN { get; set; }

        public string STATUS { get; set; }

        public ICollection<TransaksiPeminjaman> TransaksiPeminjaman { get; set; }
        public ICollection<HistoryPeminjaman> HistoryPeminjaman { get; set; }
    }
}
