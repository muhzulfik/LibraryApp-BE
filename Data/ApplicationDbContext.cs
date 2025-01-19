using library_be.Models;
using Microsoft.EntityFrameworkCore;

namespace library_be.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<MasterBuku> Masterbuku { get; set; }
        public DbSet<MasterMahasiswa> Mastermahasiswa { get; set; }
        public DbSet<InventoryBuku> Inventorybuku { get; set; }
        public DbSet<HistoryPeminjaman> Historypeminjaman { get; set; }
        public DbSet<TransaksiPeminjaman> Transaksipeminjaman { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MasterBuku>()
                .HasMany(mb => mb.InventoryBuku)
                .WithOne(ib => ib.MasterBuku)
                .HasForeignKey(ib => ib.IDBUKU);

            // MasterBuku to HistoryPeminjaman (one-to-many)
            modelBuilder.Entity<MasterBuku>()
                .HasMany(mb => mb.HistoryPeminjaman)
                .WithOne(hp => hp.MasterBuku)
                .HasForeignKey(hp => hp.IDBUKU)
                .OnDelete(DeleteBehavior.NoAction);

            // MasterMahasiswa to TransaksiPeminjaman (one-to-many)
            modelBuilder.Entity<MasterMahasiswa>()
                .HasMany(mm => mm.TransaksiPeminjaman)
                .WithOne(tp => tp.MasterMahasiswa)
                .HasForeignKey(tp => tp.NIM);

            // MasterMahasiswa to HistoryPeminjaman (one-to-many)
            modelBuilder.Entity<MasterMahasiswa>()
                .HasMany(mm => mm.HistoryPeminjaman)
                .WithOne(hp => hp.MasterMahasiswa)
                .HasForeignKey(hp => hp.NIM)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TransaksiPeminjaman>()
                .HasMany(tp => tp.HistoryPeminjaman)
                .WithOne(hp => hp.TransaksiPeminjaman)
                .HasForeignKey(hp => hp.IDTRANSAKSI)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);





        }
    }
}
