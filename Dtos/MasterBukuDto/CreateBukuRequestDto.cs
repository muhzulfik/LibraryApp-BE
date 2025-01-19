namespace library_be.Dtos.MasterBukuDto
{
    public class CreateBukuRequestDto
    {
        public string JUDUL { get; set; }

        public string PENGARANG { get; set; }

        public string? PENERBIT { get; set; }

        public string? TAHUNTERBIT { get; set; }
    }
}
