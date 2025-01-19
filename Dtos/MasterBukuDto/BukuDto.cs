using library_be.Models;
using System.ComponentModel.DataAnnotations;

namespace library_be.Dtos.MasterBukuDto
{
    public class BukuDto
    {
        public long IDBUKU { get; set; }
     
        public string JUDUL { get; set; }
        
        public string PENGARANG { get; set; }
        
        public string? PENERBIT { get; set; }

        public string? TAHUNTERBIT { get; set; }

        public ICollection<InventoryBuku> InventoryBuku { get; set; }
        public ICollection<HistoryPeminjaman> HistoryPeminjaman { get; set; }
    }
}
