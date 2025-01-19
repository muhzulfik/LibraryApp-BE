using library_be.Models;
using System.ComponentModel.DataAnnotations;

namespace library_be.Dtos.InventoryBukuDto
{
    public class InventoryDto
    {
        public long IDSTOK { get; set; }

        public long IDBUKU { get; set; }

        public string LOKASIRAK { get; set; }

        public int JUMLAHSTOK { get; set; }

        public string JudulBuku { get; set; }
    }
}
