using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_be.Models
{
    public class InventoryBuku
    {
        [Key]
        public long IDSTOK { get; set; }

        public long IDBUKU { get; set; }

        [MaxLength(200)]
        public string LOKASIRAK { get; set; }

        public int JUMLAHSTOK { get; set; }

        public MasterBuku MasterBuku { get; set; }
    }
}
