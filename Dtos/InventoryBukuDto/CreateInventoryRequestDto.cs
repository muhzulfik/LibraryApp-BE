namespace library_be.Dtos.InventoryBukuDto
{
    public class CreateInventoryRequestDto
    {
        public long IDBUKU { get; set; }

        public string LOKASIRAK { get; set; }

        public int JUMLAHSTOK { get; set; }
    }
}
