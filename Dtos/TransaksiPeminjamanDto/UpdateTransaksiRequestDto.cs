﻿namespace library_be.Dtos.TransaksiPeminjamanDto
{
    public class UpdateTransaksiRequestDto
    {
        public string NIM { get; set; }

        public DateTime TANGGALPINJAM { get; set; }

        public DateTime TANGGALKEMBALI { get; set; }
        public string STATUS { get; set; }

        public List<int> IDBUKU { get; set; }
    }
}
