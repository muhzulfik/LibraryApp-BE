namespace library_be.Dtos.MasterMahasiswaDto
{
    public class CreateMahasiswaRequestDto
    {
        public string NIM { get; set; }

        public string NAMA { get; set; }

        public string FAKULTAS { get; set; }

        public string JURUSAN { get; set; }

        public string STATUS { get; set; }
    }
}
